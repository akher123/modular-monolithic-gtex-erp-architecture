using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Models
{
    public class CustomCategoryViewModel : BaseViewModel
    {
        public CustomCategoryModel CustomCategory { get; set; }

        public CustomCategoryTypeModel CustomCategoryType { get; set; }

        public List<SelectModel> BusinessProfileSelectItems { get; set; }
        public List<SelectModel> MapTypeSelectItems { get; set; }
        //public List<SelectModel> ParentSelectItems { get; set; }
    }
}
