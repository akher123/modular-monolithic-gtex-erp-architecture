using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface ICustomCategoryService
    {
        #region Get List
               
        Task<LoadResult> GetCustomCategoryListByRoutingKeyAsync(string routingKey, DataSourceLoadOptionsBase options);
        Task<List<CustomCategoryModuleModel>> GetCustomCategoryModuleListAsync();
        Task<List<SelectModel>> GetMapTypeSelectListAsync(string routingKey);
        Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId);
        Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId, int businessProfileId);
        Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId, int[] businessProfileId);
        //Task<List<CustomCategoryTypeModel>> GetCustomCategoryTypeListAsync(List<int> ids);
        Task<List<SelectModel>> GetEntityTypeListAsync();


        #endregion

        #region Get Single Entity

        Task<CustomCategoryTypeModel> GetCustomCategoryTypeByRoutingKeyAsync(string routingKey);
        Task<CustomCategoryModel> GetCustomCategoryByIdAsync(int id);

        #endregion

        #region CUD

        Task<int> SaveCustomCategoryAsync(int id, CustomCategoryModel model);
        Task<int> DeleteEntityAsync(int id);
        Task<bool> DeleteEntitiesAsync(List<int> ids);
        
        Task<bool> MoveUpCategoriesAsync(int id);
        Task<bool> MoveDownCategoriesAsync(int id);

        #endregion
    }
}
