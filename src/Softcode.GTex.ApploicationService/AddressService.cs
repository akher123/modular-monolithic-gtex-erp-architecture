using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using DevExtreme.AspNet.Data;
using System.Reflection;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public class AddressService : BaseService<AddressService>, IAddressService
    {

        private readonly IRepository<Address> addressRepository;
        private readonly IRepository<RecordInfo> recordInfoRepository;        

        public AddressService(
             IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<Address> addressRepository
            , IRepository<RecordInfo> recordInfoRepository
            ) : base(applicationServiceBuilder)
        {
            this.addressRepository = addressRepository;
            this.recordInfoRepository = recordInfoRepository;
            
        }



        public async Task<LoadResult> GetAddressListByEntityIdAsync(Guid uniqueEntityId, int entityId, DataSourceLoadOptionsBase options)
        {
            RecordInfo record = recordInfoRepository.FindOne(r => r.Id == uniqueEntityId);

            EntityType entityType = this.ApplicationCacheService.GetEntityType().FirstOrDefault(e => e.Id == record.EntityTypeId);

            var assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name == "Softcode.GTex.Data").FirstOrDefault();
            var type = Assembly.Load(assemblyName).GetType($"Softcode.GTex.Data.Models.{entityType.AddressRelationClass}");

            MethodInfo method = typeof(Queryable).GetMethod("OfType");
            MethodInfo generic = method.MakeGenericMethod(new Type[] { type });
            var result = (IQueryable<Address>)generic.Invoke(null, new object[] { this.addressRepository.GetAll().Include(a => a.State).Include(a => a.AddressType) });

            var query = result.Where(a => a.EntityId == entityId)
                .Select(a => new
                {
                    Id = a.Id,
                    Address = a.Street
                               + (string.IsNullOrEmpty(a.Suburb) ? "" : ", " + a.Suburb)
                               + (a.State == null ? "" : ", " + a.State.StateName) 
                               + (string.IsNullOrEmpty(a.PostCode) ? "" : " " + a.PostCode)  ,
                           
                    addressType = a.AddressType.Name,                    
                    a.IsActive,
                    a.IsPrimary
                });



            return await Task.Run(() => DataSourceLoader.Load(query, options));
        }
        public async Task<List<AddressModel>> GetAddressModelListByEntityIdAsync(Guid uniqueEntityId, int entityId)
        {
            if (uniqueEntityId == Guid.Empty)
            {
                return null;
            }

            RecordInfo record = await recordInfoRepository.FindOneAsync(r => r.Id == uniqueEntityId);

            if (record == null) {
                throw new SoftcodeArgumentMissingException("Entity not found.");
            }

            EntityType entityType = this.ApplicationCacheService.GetEntityType().FirstOrDefault(e => e.Id == record.EntityTypeId);

            var assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name == "Softcode.GTex.Data").FirstOrDefault();
            var type = Assembly.Load(assemblyName).GetType($"Softcode.GTex.Data.Models.{entityType.AddressRelationClass}");

            MethodInfo method = typeof(Queryable).GetMethod("OfType");
            MethodInfo generic = method.MakeGenericMethod(new Type[] { type });
            var result = (IQueryable<Address>)generic.Invoke(null, new object[] { this.addressRepository.GetAll().Include(a => a.State).Include(a => a.AddressType) });

            List<AddressModel> address = result.Where(a => a.EntityId == entityId)
                .Select(a => new AddressModel
                {
                    Id = a.Id,
                    Street = a.Street,
                    Suburb = a.Suburb,
                    StateId = a.StateId,
                    StateName =a.State.StateName,
                    PostCode = a.PostCode,
                    AddressTypeId = a.AddressTypeId,
                    AddressType = a.AddressType.Name,
                    IsActive= a.IsActive,
                    IsPrimary= a.IsPrimary
                }).ToList();

            return address;
        }
 

        public async Task<AddressModel> GetAddressByIdAsync(Guid uniqueEntityId, int id)
        {
            if (id < 1)
            {
                //return await Task.Run(()=> new AddressModel { UniqueEntityId = uniqueEntityId });
                AddressModel emptyModel = new AddressModel { UniqueEntityId = uniqueEntityId };
                return emptyModel;
            }

            Address address = await Task.Run(()=> this.addressRepository.Where(x => x.Id == id).Include(a=>a.State).FirstOrDefault());
            if (address == null)
            {
                throw new SoftcodeArgumentMissingException("Address not found.");
            }


            AddressModel model = Mapper.Map<AddressModel>(address);
            model.UniqueEntityId = uniqueEntityId;

            return model;
        }

        public async Task<int> SaveAddressAsync(int id, AddressModel model)
        {
            RecordInfo record = recordInfoRepository.FindOne(r => r.Id == model.UniqueEntityId);

            EntityType entityType = this.ApplicationCacheService.GetEntityType().FirstOrDefault(e => e.Id == record.EntityTypeId);

            var assemblyName = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name == "Softcode.GTex.Data").FirstOrDefault();
            Type type = Assembly.Load(assemblyName).GetType($"Softcode.GTex.Data.Models.{entityType.AddressRelationClass}");
            var address = (Address)Activator.CreateInstance(type);

            if (id > 0)
            {
                MethodInfo method = typeof(Queryable).GetMethod("OfType");
                MethodInfo generic = method.MakeGenericMethod(new Type[] { type });
                //get entity address
                var result = (IQueryable<Address>)generic.Invoke(null, new object[] { this.addressRepository.GetAll() });

                address = result.Where(b => b.Id == id).FirstOrDefault();

                if (address == null)
                {
                    throw new SoftcodeArgumentMissingException("Address not found.");
                }
            }

            this.Mapper.Map(model, address);
             
            address.EntityTypeId = record.EntityTypeId;

            if (model.IsPrimary)
            {
                address.IsActive = true;
                //set default = false for previous default record 
                addressRepository.Attach(addressRepository
                                                .Where(x => x.EntityId == id && x.Id != model.Id && x.IsPrimary).ToList()
                                                .Select(x => { x.IsPrimary = false; return x; }).FirstOrDefault());
            }

            if (id == 0)
            {
                await addressRepository.CreateAsync(address);
            }
            else
            {
                await addressRepository.UpdateAsync(address);
            }

            return address.Id;
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("Address not found.");
            }

            if (!await addressRepository.ExistsAsync(x => x.Id == id))
            {
                throw new SoftcodeArgumentMissingException("Address not found.");
            }

            return await addressRepository.DeleteAsync(t => t.Id == id) > 0;
        }

        public async Task<bool> DeleteAddresssAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Address not found.");
            }

            if (!await addressRepository.ExistsAsync(x => ids.Contains(x.Id)))
            {
                throw new SoftcodeArgumentMissingException("Address not found.");
            }

            return await addressRepository.DeleteAsync(t => ids.Contains(t.Id)) > 0;
        }


    }
}
