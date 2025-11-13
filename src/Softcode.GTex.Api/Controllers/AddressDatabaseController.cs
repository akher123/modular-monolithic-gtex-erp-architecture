using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Controllers
{
    [Route("api/application-service/address-database")]
    public class AddressDatabaseController : BaseController<AddressDatabaseController>
    {
        
        private readonly IAddressDatabaseService addressDatabaseService;
        public AddressDatabaseController( IAddressDatabaseService addressDatabaseService)
        {
            this.addressDatabaseService = addressDatabaseService;
        }

        [HttpGet]
        [Route("get-country")]
        //[ActionAuthorize(2, "admin")]
        public async Task<IActionResult> GetCountryAsync()
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await addressDatabaseService.GetCountrySelectItemsAsync()
            });
        }


        [HttpGet]
        [Route("get-state-by-country/{countryId}")]       
        public async Task<IActionResult> GetStateByCountryAsync([Range(1, int.MaxValue)]int countryId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await addressDatabaseService.GetStateSelectItemsAsync(countryId)
            });
        }

        [HttpGet]
        [Route("get-postcode-by-state/{stateId}")]      
        public async Task<IActionResult> GetPostcodeByStateAsync([Range(1, int.MaxValue)]int stateId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await addressDatabaseService.GetPostcodeSelectItemsAsync(stateId)
            });
        }

        [HttpGet]
        [Route("get-suburb")]        
        public async Task<IActionResult> GetSuburbAsync()
        {
            return Ok(new ResponseMessage<List<SuburbModel>>
            {
                Result = await addressDatabaseService.GetSuburbAsync()
            });
        }
    }
}
