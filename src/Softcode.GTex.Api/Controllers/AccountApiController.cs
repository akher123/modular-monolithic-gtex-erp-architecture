using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/application-service/account")]
    public class AccountApiController : BaseController<AccountApiController>
    {
        private readonly IUserService userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public AccountApiController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-change-password")]
        public async Task<IActionResult> GetChangePasswordAsync()
        {
            return Ok(new ResponseMessage<ChangePasswordViewModel>
            {
                Result = await Task.Run(() => new ChangePasswordViewModel())
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-password")]
        [ModelValidation]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordViewModel model)
        {
            return Ok(new ResponseMessage<bool>
            {
                Result = await userService.ChangeUserPasswordAsync(model.CurrentPassword, model.NewPassword)
            });
        }
    }
}
