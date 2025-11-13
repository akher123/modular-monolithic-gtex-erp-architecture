using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/system-service/application-entity")]
    public class EntityController : BaseController<EntityController>
    {
        private readonly IEntityService entityService;

        public EntityController(IEntityService entityService)
        {
            this.entityService = entityService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityTypeId"></param>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-entities/{entityTypeId:int}/{businessProfileId:int}")]

        public async Task<IActionResult> GetEntitiesAsync(int entityTypeId, int businessProfileId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await entityService.GetEntityListByEntityTypeAsync(entityTypeId, businessProfileId)
            });
        }

        /// <summary>
        /// GetEmailServersAsync
        /// </summary>
        /// <param name="loadOptions">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-companies/{businessProfileId:int}")]

        public async Task<IActionResult> GetCompaniesAsync(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await entityService.GetEntityListByEntityTypeAsync(ApplicationEntityType.Company, businessProfileId)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contacts/{businessProfileId:int}")]

        public async Task<IActionResult> GetContactsAsync(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await entityService.GetEntityListByEntityTypeAsync(ApplicationEntityType.Contact, businessProfileId)
            });
        }
    }
}
