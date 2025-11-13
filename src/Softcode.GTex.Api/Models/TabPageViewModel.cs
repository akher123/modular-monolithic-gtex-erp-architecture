using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Models
{
    public class TabPageViewModel : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        ///public BusinessProfileModel BusinessProfileModel { get; set; }
        public List<TabModel> TabItems { get; set; }

    }
}
