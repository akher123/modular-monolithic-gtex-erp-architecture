using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/system-settings/security-profile")]
    public class SecurityProfileController : BaseController<SecurityProfileController>
    {
        private readonly ISecurityProfileService securityProfileService;
        private readonly IBusinessCategoryService businessCategoryService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityProfileService"></param>
        /// <param name="businessCategoryService"></param>
        public SecurityProfileController(ISecurityProfileService securityProfileService
            , IBusinessCategoryService businessCategoryService
            )
        {
            this.securityProfileService = securityProfileService;
            this.businessCategoryService = businessCategoryService;
        }

        /// <summary>
        /// GetSecurityPrifileAsync
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-security-profiles")]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.SecurityProfileList)]
        public async Task<IActionResult> GetSecurityPrifileAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await securityProfileService.GetSecurityPrifileListAsync(loadOptions) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-security-profile/{id}")]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.ViewSecurityProfile)]
        public async Task<IActionResult> GetSecurityProfileAsync(int id)
        {
            return Ok(new ResponseMessage<SecurityProfileViewModel>
            {
                Result = new SecurityProfileViewModel
                {
                    SecurityProfile = await securityProfileService.GetSecurityProfileByIdAsync(id),
                    PasswordCombinationTypeSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.PasswordCombinationType)
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-security-profile")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.CreateSecurityProfile)]
        public async Task<IActionResult> CreateSecurityProfileAsync([FromBody] SecurityProfileModel model)
        {
            ResponseMessage<int> response = new ResponseMessage<int>();
            response.Result = await securityProfileService.SaveSecurityProfileDetailsAsync(0, model);
            return Ok(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-security-profile/{id:int}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.UpdateSecurityProfile)]
        public async Task<IActionResult> UpdateSecurityProfileAsync(int id, [FromBody] SecurityProfileModel model)
        {
            ResponseMessage<int> response = new ResponseMessage<int>();
            response.Result = await securityProfileService.SaveSecurityProfileDetailsAsync(id, model);
            return Ok(response);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-security-profile/{id:int}")]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.DeleteSecurityProfile)]
        public async Task<IActionResult> DeleteSecurityProfileAsync(int id)
        {
            return Ok(await securityProfileService.DeleteSecurityProfileAsync(id));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-security-profiles")]
        [ActionAuthorize(ApplicationPermission.SecurityProfile.DeleteSecurityProfile)]
        public async Task<IActionResult> DeleteSecurityProfilesAsync([FromBody] List<int> ids)
        {
            return Ok(await securityProfileService.DeleteSecurityProfilesAsync(ids));
        }
    }
}
