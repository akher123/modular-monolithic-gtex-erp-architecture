using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{

    public class BusinessCategoryService: BaseService<BusinessCategoryService>, IBusinessCategoryService
    {
        private readonly IRepository<BusinessCategory> businessCategoryRepository;
        private readonly IRepository<BusinessCategoryType> businessCategoryTypeRepository;
        

        public BusinessCategoryService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<BusinessCategory> businessCategoryRepository
            , IRepository<BusinessCategoryType> businessCategoryTypeRepository
            ) : base(applicationServiceBuilder)
        {
            this.businessCategoryRepository = businessCategoryRepository;
            this.businessCategoryTypeRepository = businessCategoryTypeRepository;            
        }

        public List<SelectModel> GetBusinessCategoryByType(int typeId)
        {
            var types = this.ApplicationCacheService.GetBusinessCategoryType();
            return types.Where(x => x.Id == typeId).First().BusinessCategories.Where(c=>!c.IsInternal).Select(x => new SelectModel {
                Id=x.Id,
                Name=x.Name,
                IsDefault=x.IsDefault
            }).ToList();
        }

        public Task<List<SelectModel>> GetBusinessCategoryByTypeAsync(int typeId)
        {
            var types = this.ApplicationCacheService.GetBusinessCategoryType();
            return Task.Run(()=> types.Where(x => x.Id == typeId).First().BusinessCategories.Where(c => !c.IsInternal).OrderBy(x => x.RowNo).Select(x => new SelectModel
            {
                Id = x.Id,
                Name = x.Name,
                IsDefault = x.IsDefault
            }).ToList());
        }
    }
}
