using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    public class ApplicationPageViewModel : BaseViewModel
    {
        public ApplicationPageViewModel()
        {
            PageTypeDataSource = new List<SelectModel>();
            PageTypeDataSource.Add(new SelectModel { Id = "List", Name = "List" });
            PageTypeDataSource.Add(new SelectModel { Id = "Detail", Name = "Detail" });
        }
        public ApplicationPageModel ApplicationPage { get; set; }
        public ApplicationPageModel EmptyApplicationPage { get; set; }
        public List<SelectModel> PageTypeDataSource { get; set; }
        //public List<SelectModel> PageType { get; set; }

    }
}
