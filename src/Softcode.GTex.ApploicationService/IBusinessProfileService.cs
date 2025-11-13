using System;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IBusinessProfileService
    {
        Task<LoadResult> GetBusinessProfileListAsync(DataSourceLoadOptionsBase options);
        Task<BusinessProfileModel> GetBusinessProfileModelByIdAsync(int id);
        Task<List<SelectModel>> GetUserBusinessProfileSelectItemsAsync();
        Task<List<SelectModel>> GetContactBusinessProfileSelectItemsByUserBPIdsAsync();
        List<TabModel> GetBusinessProfileDetailsTabs(int id);
        Task<bool> DeleteBusinessProfileByIdAsync(int id);
        Task<bool> DeleteBusinessProfilesAsync(List<int> ids);
        Task<int> SaveBusinessProfileAsync( BusinessProfileModel businessProfileModel);
        
    }
}
