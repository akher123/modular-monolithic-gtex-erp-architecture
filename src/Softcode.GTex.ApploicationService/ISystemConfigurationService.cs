using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface ISystemConfigurationService
    {
        Task<SecurityConfigurationModel> GetSecurityConfigurationModelAsync();
        Task<int> SetSecurityConfigurationValuesAsync(SecurityConfigurationModel model);
        List<SelectModel> GetUserAuthenticationTypeSelectItems();
        List<SelectModel> GetUserNameTypeSelectedItem();
    }
}
