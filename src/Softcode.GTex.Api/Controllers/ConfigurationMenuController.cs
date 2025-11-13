using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Softcode.GTex.Api.Controllers
{
    /// <summary>
    /// ConfigurationMenuController controller. this api is liable to provide configuration related api
    /// </summary>
    [Route("api/system-settings/configuration-menu")]
    public class ConfigurationMenuController : BaseController<ConfigurationMenuController>
    {
        public ConfigurationMenuController()
        {
        }
        /// <summary>
        /// get configuration menu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-configurations-link-menu")]
        [AllowAnonymous]
        public IActionResult GetConfigurationMenu()
        {
            MenuPageViewModel model = new MenuPageViewModel();
            model.MenuItems.Add(new MenuItemModel {
                Name= "System Configuration",
                HelpText = "Manage System Configurations",
                NavigateUrl = "/system-settings/system-configuration"
            });

            model.MenuItems.Add(new MenuItemModel
            {
                Name = "Menu Customisation",
                HelpText = "Manage Application Menu",
                NavigateUrl = "/"
            });

            ResponseMessage<MenuPageViewModel> response = new ResponseMessage<MenuPageViewModel>
            {
                Result = model
            };
            return Ok(response);
        }
    }
}
