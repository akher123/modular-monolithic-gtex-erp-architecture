using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IAddressDatabaseService
    {
        Task<List<SelectModel>> GetTimeZoneSelectItemsAsync();
        Task<List<SelectModel>> GetCountrySelectItemsAsync();
        Task<List<SelectModel>> GetStateSelectItemsAsync(int countryId);
        Task<List<SelectModel>> GetPostcodeSelectItemsAsync(int stateIdv);
        Task<List<SuburbModel>> GetSuburbAsync();
        Task<List<SelectModel>> GetRegionAsync(int businessProfileId);

    }
}
