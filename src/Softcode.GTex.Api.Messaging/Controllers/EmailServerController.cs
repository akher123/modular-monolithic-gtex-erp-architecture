using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Messaging.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicantionService.Messaging;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Messaging.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/messaging/email-server")]
    public class EmailServerController : BaseController<EmailServerController>
    {
        private readonly IServerSettingService serverService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly IBusinessCategoryService businessCategoryService;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverService"></param>
        /// <param name="businessProfileService"></param>
        /// <param name="businessCategoryService"></param>
        public EmailServerController(IServerSettingService serverService
            , IBusinessProfileService businessProfileService
            , IBusinessCategoryService businessCategoryService
            )
        {
            this.serverService = serverService;
            this.businessProfileService = businessProfileService;
            this.businessCategoryService = businessCategoryService;
        }

        /// <summary>
        /// Get Email Server list Async
        /// </summary>
        /// <param name="loadOptions">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-email-server-list")]
        public async Task<IActionResult> GetEmailServersAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult>
            {
                Result = await serverService.GetServerSettingListAsync(loadOptions) 
            });
        }

        /// <summary>
        /// Get Email Server by Id Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-email-server/{id:int}")]
        public async Task<IActionResult> GetEmailServerByIdAsync(int id)
        {
            ResponseMessage<EmailServerViewModel> response = new ResponseMessage<EmailServerViewModel>
            {
                Result = new EmailServerViewModel
                {
                    EmailServer = await serverService.GetServerSettingAsync(id),
                    BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                    AuthenticationTypeSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.EmailServerAuthenticationType),
                    ProtocolSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.EmailServerProtocol),
                    SenderOptionSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.EmailServerSenderOption),
                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
                }
            };

            if (id < 1)
            {
                // for new item set default value
                response.Result.EmailServer.AuthenticationTypeId = response.Result.AuthenticationTypeSelectItems.FirstOrDefault(x => x.IsDefault)?.Id.ToInt() ?? 0;
                response.Result.EmailServer.ProtocolId = response.Result.ProtocolSelectItems.FirstOrDefault(x => x.IsDefault)?.Id.ToInt() ?? 0;
                response.Result.EmailServer.SenderOptionId = response.Result.SenderOptionSelectItems.FirstOrDefault(x => x.IsDefault)?.Id.ToInt() ?? 0;
            }

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-email-server")]
        [ModelValidation]        
        public async Task<IActionResult> CreateBusinessProfileCreate([FromBody] EmailServerModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await serverService.SaveServerSettingAsync(0, model)
            });
        }

        /// <summary>
        /// Update security configuration
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-email-server/{id:int}")]
        [ModelValidation]        
        public async Task<IActionResult> UpdateBusinessProfileCreate(int id, [FromBody] EmailServerModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await serverService.SaveServerSettingAsync(id, model)
            });
        }



        /// <summary>
        /// Delete Email Server Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-email-server/{id:int}")]
        public async Task<IActionResult> DeleteEmailServerAsync(int id)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await serverService.DeleteEmailServerAsync(id)
            });
        }

        /// <summary>
        /// SendTestEmail
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("send-test-email")]
        public async Task<IActionResult> SendTestEmail([FromBody] EmailServerModel model)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await Task.Run(()=> serverService.SendTestEmail(model))
            });
        }

    }
}
