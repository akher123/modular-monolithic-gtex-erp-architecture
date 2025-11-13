using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Softcode.GTex.Configuration;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// SystemConfigurationController
    /// </summary>
    [Route("api/system-settings/system-configuration")]
    public class SystemConfigurationController : BaseController<SystemConfigurationController>
    {
        private readonly ISystemConfigurationService configurationService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationService"></param>
        public SystemConfigurationController(ISystemConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Get system security configuration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-system-security-configuration")]
        [ActionAuthorize(ApplicationPermission.Configuration.ViewConfiguration)]
        public async Task<IActionResult> GetSystemSecurityConfiguration()
        {
            return Ok(new ResponseMessage<SecurityConfigurationViewModel>
            {
                Result = new SecurityConfigurationViewModel
                {
                    SecurityConfiguration = await configurationService.GetSecurityConfigurationModelAsync(),
                    AuthenticationTypeSelectItems = configurationService.GetUserAuthenticationTypeSelectItems(),
                    UserNameTypeSelectItems = configurationService.GetUserNameTypeSelectedItem()
                }
            });
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-system-security-configuration")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Configuration.UpdateConfiguration)]
        public async Task<IActionResult> UpdateSecurityConfigurationAsync([FromBody] SecurityConfigurationModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await configurationService.SetSecurityConfigurationValuesAsync(model)
            });
        }
    }
}
