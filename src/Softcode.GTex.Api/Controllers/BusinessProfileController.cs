using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Softcode.GTex.Configuration;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/system-settings/business-profile")]
    public class BusinessProfileController : BaseController<BusinessProfileController>
    {
        private readonly IBusinessProfileService businessProfileService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileService"></param>
        public BusinessProfileController(IBusinessProfileService businessProfileService)
        {
            this.businessProfileService = businessProfileService;
        }

        /// <summary>
        /// Get Business Profile Async
        /// </summary>
        /// <param name="loadOptions">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-business-profiles")]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.ShowBusinessProfile)]
        public async Task<IActionResult> GetBusinessProfilesAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult>
            {
                Result = await businessProfileService.GetBusinessProfileListAsync(loadOptions)
            });
        }

        /// <summary>
        /// Get Business Profile Details Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-business-profile-details")]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.ViewBusinessProfileDetails)]
        public async Task<IActionResult> GetBusinessProfileDetailsAsync(int id)
        {
            return Ok(new ResponseMessage<TabPageViewModel>
            {
                Result = await Task.Run(() => new TabPageViewModel
                {
                    TabItems = businessProfileService.GetBusinessProfileDetailsTabs(id),
                })
            });
        }

        /// <summary>
        /// Get Business Profile Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-business-profile")]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.ViewBusinessProfileDetails)]
        public async Task<IActionResult> GetBusinessProfileAsync(int id)
        {
            return Ok(new ResponseMessage<BusinessProfileViewModel>
            {
                Result = new BusinessProfileViewModel
                {
                    BusinessProfileModel = await businessProfileService.GetBusinessProfileModelByIdAsync(id),
                    EmptyBusinessProfileModel = new BusinessProfileModel(),
                    IsDefaultBusinessProfile = this.LoggedInUser.IsDefaultBusinessProfile
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-business-profile")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.CreateBusinessProfile)]
        public async Task<IActionResult> CreateBusinessProfileCreate([FromBody] BusinessProfileModel businessProfile)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await businessProfileService.SaveBusinessProfileAsync(businessProfile)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-business-profile/{id:int}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.UpdateBusinessProfile)]
        public async Task<IActionResult> UpdateBusinessProfileCreate(int id, [FromBody] BusinessProfileModel businessProfile)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await businessProfileService.SaveBusinessProfileAsync(businessProfile)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-business-profile/{id:int}")]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.DeleteBusinessProfile)]
        public async Task<IActionResult> DeleteBusinessProfileAsync(int id)
        {
            return Ok(await businessProfileService.DeleteBusinessProfileByIdAsync(id));
        }

        /// <summary>
        /// delete business profile
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-business-profiles")]
        [ActionAuthorize(ApplicationPermission.BusinessProfile.DeleteBusinessProfile)]
        public async Task<IActionResult> DeleteBusinessProfilesAsync([FromBody]List<int> ids)
        {
            return Ok(await businessProfileService.DeleteBusinessProfilesAsync(ids));
        }
    }
}
