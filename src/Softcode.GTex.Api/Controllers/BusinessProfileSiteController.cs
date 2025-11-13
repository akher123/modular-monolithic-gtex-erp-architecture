using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/service/business-profile-site")]
    public class BusinessProfileSiteController : BaseController<BusinessProfileSiteController>
    {
        private readonly IBusinessProfileSiteService businessProfileSiteService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileSiteService"></param>
        public BusinessProfileSiteController(IBusinessProfileSiteService businessProfileSiteService)
        {
            this.businessProfileSiteService = businessProfileSiteService;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-business-profile-site-select-items/{businessProfileId:int}")]

        public async Task<IActionResult> GetGetCostCentreSelectItemsAsync(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<BusinessProfileSiteSelectModel>>
            {
                Result = await businessProfileSiteService.GetBusinessProfileSiteListAsync(businessProfileId)
            });
        }




    }
}
