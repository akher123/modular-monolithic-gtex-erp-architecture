using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.Web.Api.Controllers
{
    [Route("api/client-application")]
    public class ClientApplicationController : ControllerBase
    {

        [HttpGet]
        [Route("admin-application-angular-client")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAdminApplicationAngularClient()
        {
            ClientApplicationConfig clientApplicationConfig = ItmConfigurations.ClientApplicationConfigs.FirstOrDefault(x => x.id.Equals("admin-application", StringComparison.InvariantCulture));

            return Ok(clientApplicationConfig);
        }
    }
}