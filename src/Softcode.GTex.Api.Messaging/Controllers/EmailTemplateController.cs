using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Providers;

namespace Softcode.GTex.Api.Messaging.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/messaging-service/email-template")]
    public class EmailTemplateController : BaseController<EmailTemplateController>
    {
        ///// <summary>
        ///// 
        ///// </summary>
        //public EmailTemplateController()
        //{
        //}

        ///// <summary>
        ///// 
        ///// </summary>        
        ///// <returns></returns>
        //[HttpGet]
        //[Route("get-email-templates")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetEmailTemplateList()
        //{
        //    //return Ok(new ResponseMessage<ApplicationListPageModel> { Result = await applicationPageService.GetApplicationListPageByRoutingUrlAsync(routingUrl) });
        //    throw new NotImplementedException();

        //}
    }
}
