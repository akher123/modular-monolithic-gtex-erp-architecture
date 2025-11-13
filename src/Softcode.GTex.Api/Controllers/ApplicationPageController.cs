using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    [Route("api/application-service/application-page")]
    public class ApplicationPageController : BaseController<ApplicationPageController>
    {
        private readonly IApplicationPageService applicationPageService;
        public ApplicationPageController(IApplicationPageService applicationPageService)
        {
            this.applicationPageService = applicationPageService;
        }

        /// <summary>
        /// get application page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-application-list-page-by-routing-url")]
        [Authorize]
        public async Task<IActionResult> GetApplicationListPageByRoutingUrl(string routingUrl)
        {
            return Ok(new ResponseMessage<ApplicationListPageModel> { Result = await applicationPageService.GetApplicationListPageByRoutingUrlAsync(routingUrl) });
        }

        /// <summary>
        /// get application page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-application-list-page-by-name")]
        [Authorize]
        public async Task<IActionResult> GetApplicationListPageByName(string name)
        {
            return Ok(new ResponseMessage<ApplicationListPageModel> { Result = await applicationPageService.GetApplicationListPageByNameAsync(name) });
        }

        /// <summary>
        /// get application page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-application-detail-page-by-routing-url")]
        [Authorize]
        public async Task<IActionResult> GetApplicationDetailPageByRoutingUrl(string routingUrl)
        {
            return Ok(new ResponseMessage<ApplicationDetailPageModel> { Result = await applicationPageService.GetApplicationDetailPageByRoutingUrlAsync(routingUrl) });
        }

        /// <summary>
        /// get application page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-application-detail-page-by-name")]
        [Authorize]
        public async Task<IActionResult> GetApplicationDetailPageByName(string name)
        {
            return Ok(new ResponseMessage<ApplicationDetailPageModel> { Result = await applicationPageService.GetApplicationDetailPageByNameAsync(name) });
        }


        [HttpGet]
        [Route("get-application-list-page-by-id/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetApplicationListPageByid(int id)
        {
            ResponseMessage<ApplicationPageViewModel> response = new ResponseMessage<ApplicationPageViewModel>() { Result = new ApplicationPageViewModel() };
            response.Result.ApplicationPage = await applicationPageService.GetApplicationListPageByIdAsync(id);
            
            return Ok(response);
        }

        [HttpGet]
        [Route("get-application-detail-page-by-id/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetApplicationDetailPageByid(int id)
        {
            ResponseMessage<ApplicationPageViewModel> response = new ResponseMessage<ApplicationPageViewModel>();
            response.Result.ApplicationPage = await applicationPageService.GetApplicationDetailPageByIdAsync(id);
            
            return Ok(response);
        }




    }
}
