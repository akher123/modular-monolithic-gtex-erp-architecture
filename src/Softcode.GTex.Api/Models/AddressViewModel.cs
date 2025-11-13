using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Models
{
    public class AddressViewModel: BaseViewModel
    {
        public AddressModel Address { get; set; }
        public List<SelectModel> AddressTypeSelectItems { get; set; }        
        public List<SelectModel> StateSelectItems { get; set; }
        public List<SelectModel> CountrySelectItems { get; set; }
        public List<SuburbModel> SuburbDataSource { get; set; }

    }
}
