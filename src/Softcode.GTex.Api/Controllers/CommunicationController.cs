using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/service/communication")]
    public class CommunicationController : BaseController<CommunicationController>
    {
        private readonly ICommunicationService communicationService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IBusinessProfileService businessProfileService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communicationService"></param>
        /// <param name="customCategoryService"></param>
        /// <param name="businessProfileService"></param>
        public CommunicationController(ICommunicationService communicationService
            , ICustomCategoryService customCategoryService
            , IBusinessProfileService businessProfileService)
        {
            this.communicationService = communicationService;
            this.customCategoryService = customCategoryService;
            this.businessProfileService = businessProfileService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-communication-list")]
        [ActionAuthorize(ApplicationPermission.NotesAndComms.ShowNoteAndCommList)]
        public async Task<IActionResult> GetCommunicationListAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult>
            {
                Result = await communicationService.GetCommunicationListAsync(loadOptions)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-communication/{id:int}")]
        [ActionAuthorize(ApplicationPermission.NotesAndComms.ViewNoteAndCommDetail)]
        public async Task<IActionResult> GetCommunicationAsync(int id)
        {
            return Ok(new ResponseMessage<CommunicationViewModel>
            {
                Result = new CommunicationViewModel
                {
                    CommunicationModel = await communicationService.GetCommunicationModelByIdAsync(id),
                    BusinessProfileSelectItems = await businessProfileService.GetUserBusinessProfileSelectItemsAsync(),
                    CommunicationForSelectItems = await customCategoryService.GetEntityTypeListAsync(),
                    CommunicationMathodSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.CommunicationMethod),
                    CommunicationStatusSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.CommunicationStatus)
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communicationModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-communication")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.NotesAndComms.CreateNoteAndCommList)]
        public async Task<IActionResult> CreateCommunicationAsync([FromBody] CommunicationModel communicationModel)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await communicationService.SaveCommunicationAsync(0, communicationModel)
            });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="communicationModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-business-profile/{id:int}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.NotesAndComms.UpdateNoteAndCommList)]
        public async Task<IActionResult> UpdateCommunicationAsync(int id, [FromBody] CommunicationModel communicationModel)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await communicationService.SaveCommunicationAsync(id, communicationModel)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-communication/{id:int}")]
        [ActionAuthorize(ApplicationPermission.NotesAndComms.DeleteNoteAndCommList)]
        public async Task<IActionResult> DeleteCommunicationAsync(int id)
        {
            return Ok(await communicationService.DeleteCommunicationAsync(id));
        }
    }
}
