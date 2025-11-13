using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Controllers
{
    [Route("api/application-service/application-menu")]
    public class ApplicationMenuController : BaseController<ApplicationMenuController>
    {
        private readonly IApplicationMenuService applicationMenuService;
        public ApplicationMenuController(IApplicationMenuService applicationMenuService)
        {
            this.applicationMenuService = applicationMenuService;
        }

        /// <summary>
        /// get application menu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-application-menu")]
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicationMenu()
        {
            return Ok(new ResponseMessage<ApplicationMenuViewModel>
            {
                Result = new ApplicationMenuViewModel
                {
                    ApplicationMenu = applicationMenuService.GetApplicationMenuAsync().Result,
                    ApplicationHeader = await applicationMenuService.GetApplicationHeaderAsync()
                }
            });
        }
        /// <summary>
        /// Create Application Menu 
        /// </summary>
        /// <param name="model">Application Menu Model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-application-menu")]
        [ModelValidation]
        [AllowAnonymous]
        public async Task<IActionResult> CreateApplicationMenuAsync([FromBody] ApplicationMenuModel model)
        {
            return Ok(new ResponseMessage<int> { Result = await applicationMenuService.CreateApplicationMenuAsync(model) });
        }
        /// <summary>
        /// Update Application Menu
        /// </summary>
        /// <param name="id">Application Menu Id</param>
        /// <param name="model">Application Menu Model</param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-application-menu/{id:int}")]
        [ModelValidation]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateApplicationMenuAsync(int id, [FromBody] ApplicationMenuModel model)
        {
            return Ok(new ResponseMessage<int> { Result = await applicationMenuService.UpdateApplicationMenuAsync(id,model) });
        }

        /// <summary>
        /// Application Menu Tree List 
        /// </summary>
        /// <returns>Application Menu Tree List</returns>
        [HttpGet]
        [Route("get-application-menu-tree-list")]
        public async Task<IActionResult> ApplicationMenuTreeListAsync()
        {
            return Ok(new ResponseMessage<List<TreeModel>>
            {
                Result = await applicationMenuService.ApplicationMenuTreeListAsync()
            });
        }

    }
}
