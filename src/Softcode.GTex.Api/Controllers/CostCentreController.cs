using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
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
    [Route("api/service/cost-centre")]
    public class CostCentreController : BaseController<CostCentreController>
    {
        private readonly ICostCentreService costCentreService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="costCentreService"></param>
        public CostCentreController(ICostCentreService costCentreService)
        {
            this.costCentreService = costCentreService;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-cost-centre-select-items/{businessProfileId:int}")]

        public async Task<IActionResult> GetGetCostCentreSelectItemsAsync(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await costCentreService.GetCostCentreSelectItemsAsync(businessProfileId)
            });
        }
    }
}
