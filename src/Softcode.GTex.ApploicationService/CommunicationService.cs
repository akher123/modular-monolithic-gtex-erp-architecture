using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class CommunicationService : BaseService<CommunicationService>, ICommunicationService
    {
        private readonly IRepository<Communication> communicationRepository;
        private readonly IRepository<DocumentFileStore> documentFileStoreRepository;
        private readonly IRepository<DocumentMetadata> documentMetadataRepository;

        public CommunicationService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<Communication> communicationRepository
            , IRepository<DocumentFileStore> documentFileStoreRepository
            , IRepository<DocumentMetadata> documentMetadataRepository)
            : base(applicationServiceBuilder)
        {
            this.communicationRepository = communicationRepository;
            this.documentFileStoreRepository = documentFileStoreRepository;
            this.documentMetadataRepository = documentMetadataRepository;
        }

        public async Task<LoadResult> GetCommunicationListAsync(DataSourceLoadOptionsBase options)
        {
            IQueryable<Communication> usersQuery = communicationRepository.AsQueryable()
              ;//.Include(u => u.Contact).Include(c => c.UserBusinessProfiles);

            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                usersQuery = usersQuery.Where(x => x.BusinessProfileId== businessProfileId);
            }

            options.Select = new[] { "Id", "Subject", "CommunicationDateTime", "EntityType.Name", "IsFollowupEnable", "Status.Name", "MethodType.Name" };
            return await Task.Run(() => DataSourceLoader.Load(usersQuery, options));

            //options.Select = new[] { "Id", "Subject", "EntityType.Name",  "IsFollowupEnable", "Status.Name", "MethodType.Name" };
            //return await communicationRepository.GetDevExpressListAsync(options);
        }

        public async Task<CommunicationModel> GetCommunicationModelByIdAsync(int id)
        {
            if (id < 1)
            {
               return new CommunicationModel();
            }

            Communication communication = await communicationRepository.FindOneAsync(x => x.Id == id);

            if(communication == null)
            {
                throw new SoftcodeNotFoundException("Communication not found");
            }

            return Mapper.Map<CommunicationModel>(communication);
        }

        public async Task<int> SaveCommunicationAsync(int id, CommunicationModel communicationModel)
        {
            if (communicationModel == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid communication");
            }

            Communication communication = new Communication();

            if(id > 0)
            {
                communication = await communicationRepository.Where(x => x.Id == id).Include(x => x.CommunicationFileStores).FirstOrDefaultAsync();

                if(communication == null)
                {
                    throw new SoftcodeNotFoundException("Communication not found");
                }
            }

            Mapper.Map(communicationModel, communication);

            if (!communication.IsFollowupEnable)
            {
                communication.FollowupByContactId = null;
                communication.FollowupDate = null;
            }

            if(id > 0)
            {
                return communicationRepository.UpdateAsync(communication).Result.Id;
            }
            return communicationRepository.CreateAsync(communication).Result.Id;
        }

        public async Task<bool> DeleteCommunicationAsync(int id)
        {
            if (id < 1)
            {
                throw new SoftcodeArgumentMissingException("Invalid communication to delete");
            }

            if (!await communicationRepository.ExistsAsync(x => x.Id == id))
            {
                throw new SoftcodeNotFoundException("Communication not found");
            }

            List<DocumentMetadata> documentMetadatas = documentMetadataRepository.Where(x => x.EntityTypeId == ApplicationEntityType.Communication && x.EntityId == id).ToList();
            if (documentMetadatas.Any())
            {
                int[] documentMetadataIds = documentMetadatas.Select(x => x.Id).ToArray();
                List<DocumentFileStore> documentFileStores = documentFileStoreRepository.Where(x => documentMetadataIds.Contains(x.DocumentMetadataId)).ToList();

                documentMetadataRepository.Remove(documentMetadatas);

                if(documentFileStores.Any())
                {
                    documentFileStoreRepository.Remove(documentFileStores);
                }
            }

            return await communicationRepository.DeleteAsync(x => x.Id == id) > 0;


        }
    }
}
