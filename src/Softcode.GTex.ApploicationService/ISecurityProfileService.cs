using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface ISecurityProfileService
    {
        Task<LoadResult> GetSecurityPrifileListAsync(DataSourceLoadOptionsBase options);
        Task<List<SelectModel>> GetSecurityProfileSelectItemsAsync();
        Task<SecurityProfileModel> GetSecurityProfileByIdAsync(int id);        
        Task<int> SaveSecurityProfileDetailsAsync(int id, SecurityProfileModel model);
        Task<int> DeleteSecurityProfileAsync(int id);
        Task<bool> DeleteSecurityProfilesAsync(List<int> ids);
        Task<Dictionary<string, string>> CheckPasswordAsync(int securityProfileId, string password, string userName, string userFullName);
    }
}
