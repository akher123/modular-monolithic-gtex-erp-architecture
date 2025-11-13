using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Softcode.GTex.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.Web.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAppSettingsController : ControllerBase
    {
        private readonly ClientAppSettings _clientAppSettings;

        public ClientAppSettingsController(IOptions<ClientAppSettings> clientAppSettings)
        {
            _clientAppSettings = clientAppSettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_clientAppSettings);
        }
    }
}
