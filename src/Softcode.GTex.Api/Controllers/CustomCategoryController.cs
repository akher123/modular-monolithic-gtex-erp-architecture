using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/system-settings/custom-categories")]
    public class CustomCategoryController : BaseController<CustomCategoryController>
    {
        private readonly ICustomCategoryService customCategoryService;
        private readonly IBusinessProfileService businessProfileService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customCategoryService"></param>
        /// <param name="businessProfileService"></param>
        public CustomCategoryController(ICustomCategoryService customCategoryService
            , IBusinessProfileService businessProfileService)
        {
            this.customCategoryService = customCategoryService;
            this.businessProfileService = businessProfileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-custom-categories-types")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypesAndCategoriesTypesAsync()
        {
            ResponseMessage<CustomCategoryModuleViewModel> response = new ResponseMessage<CustomCategoryModuleViewModel>();
            response.Result = new CustomCategoryModuleViewModel
            {
                CustomCategoryModules = await customCategoryService.GetCustomCategoryModuleListAsync()
            };

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingKey"></param>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-custom-category-list/{routingKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypeAndCategoryList(string routingKey, DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await customCategoryService.GetCustomCategoryListByRoutingKeyAsync(routingKey, loadOptions) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-custom-category-type-by-key/{routingKey}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTypeAndCategoryTypeByRoutingKey(string routingKey)
        {
            return Ok(new ResponseMessage<CustomCategoryTypeModel> { Result = await customCategoryService.GetCustomCategoryTypeByRoutingKeyAsync(routingKey) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-custom-category")]
        public async Task<IActionResult> GetCustomCategoryAsync(int id, string routingKey)
        {
            ResponseMessage<CustomCategoryViewModel> response = new ResponseMessage<CustomCategoryViewModel>();
            response.Result = await Task.Run(() => new CustomCategoryViewModel
            {
                CustomCategory = customCategoryService.GetCustomCategoryByIdAsync(id).Result,
                BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                CustomCategoryType = customCategoryService.GetCustomCategoryTypeByRoutingKeyAsync(routingKey).Result,
                MapTypeSelectItems = customCategoryService.GetMapTypeSelectListAsync(routingKey).Result,
                IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
            });

            if (id == 0)
            {
                response.Result.CustomCategory.CustomCategoryTypeId = response.Result.CustomCategoryType.Id;
                if (!LoggedInUser.IsDefaultBusinessProfile)
                {
                    response.Result.CustomCategory.BusinessProfileId = LoggedInUser.DefaultBusinessProfileId;
                }
            }

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-custom-category")]
        [ModelValidation]
        public async Task<IActionResult> CreateCustomCategoryAsync([FromBody] CustomCategoryModel model)
        {
            ResponseMessage<int> response = new ResponseMessage<int>();
            response.Result = await customCategoryService.SaveCustomCategoryAsync(0, model);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-custom-category/{id:int}")]
        [ModelValidation]
        public async Task<IActionResult> UpdateCustomCategoryAsync(int id, [FromBody] CustomCategoryModel model)
        {
            ResponseMessage<int> response = new ResponseMessage<int>();
            response.Result = await customCategoryService.SaveCustomCategoryAsync(id, model);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-custom-category/{id:int}")]
        public async Task<IActionResult> DeleteCustomCategoryAsync(int id)
        {
            return Ok(await customCategoryService.DeleteEntityAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-custom-categories")]
        public async Task<IActionResult> DeleteCustomCategoriesAsync([FromBody] List<int> ids)
        {
            return Ok(await customCategoryService.DeleteEntitiesAsync(ids));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("move-up/{id:int}")]
        [ModelValidation]
        public async Task<IActionResult> MoveUpCategoriesAsync(int id)
        {
            return Ok(await customCategoryService.MoveUpCategoriesAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("move-down/{id:int}")]
        [ModelValidation]
        public async Task<IActionResult> MoveDownCategoriesAsync(int id)
        {
            return Ok(await customCategoryService.MoveDownCategoriesAsync(id));
        }
    }
}
