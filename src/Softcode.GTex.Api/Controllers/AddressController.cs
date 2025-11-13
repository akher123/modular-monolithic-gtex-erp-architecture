using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Controllers
{
    [Route("api/application-service/address")]
    public class AddressController : BaseController<AddressController>
    {
        private readonly IAddressService addressService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IAddressDatabaseService addressDatabaseService;
        public AddressController(IAddressService addressService, ICustomCategoryService customCategoryService, IAddressDatabaseService addressDatabaseService)
        {
            this.addressService = addressService;
            this.customCategoryService = customCategoryService;
            this.addressDatabaseService = addressDatabaseService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueRowId"></param>
        /// <param name="entityId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-list-by-entityid/{uniqueRowId:Guid}/{entityId:int}")]
        public async Task<IActionResult> GetAddressListByEntityId(Guid uniqueRowId, int entityId, DataSourceLoadOptions options)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await addressService.GetAddressListByEntityIdAsync(uniqueRowId, entityId, options) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueRowId"></param>
        /// <param name="entityId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-model-list-by-entityid/{uniqueRowId:Guid}/{entityId:int}")]
        public async Task<IActionResult> GetAddressmodelListByEntityId(Guid uniqueRowId, int entityId)
        {
            return Ok(new ResponseMessage<List<AddressModel>> { Result = await addressService.GetAddressModelListByEntityIdAsync(uniqueRowId, entityId) });
        }

        /// <summary>
        /// Get Address By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-by-id/{id:int}/{uniqueEntityId:Guid}/{entityId:int}/{businessProfileId:int}")]

        public async Task<IActionResult> GetAddressById(int id, Guid uniqueEntityId, int entityId, int businessProfileId)
        {

            AddressViewModel model = new AddressViewModel
            {
                Address = await addressService.GetAddressByIdAsync(uniqueEntityId, id),
            };
            model.Address.EntityId = entityId;
            return Ok(new ResponseMessage<AddressViewModel> { Result = model });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-Type-list/{businessProfileId:int}")]
        public async Task<IActionResult> GetAddressTypeSelectBoxData(int businessProfileId)
        {
            ResponseMessage<List<SelectModel>> response = new ResponseMessage<List<SelectModel>>();
            if (businessProfileId > 0)
            {
                response.Result = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.AddressType, businessProfileId);
            }
            else
            {
                response.Result = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.AddressType);
            }

            return Ok(response);
        }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="bps"></param>
      /// <returns></returns>
        [HttpGet]
        [Route("get-address-Type-list-by-bpids")]
        public async Task<IActionResult> GetAddressTypeSelectBoxDataByBPIds([FromQuery] int[] bps)
        {
            ResponseMessage<List<SelectModel>> response = new ResponseMessage<List<SelectModel>>();
            if (bps.Length > 1)
            {
                response.Result = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.AddressType, bps).Result;
            }
            else if (bps.Length == 1)
            {
                response.Result = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.AddressType, bps[0]).Result;
            }
            else
            {
                response.Result = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.AddressType);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Address By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-init-address-data/{uniqueEntityId:Guid}")]

        public async Task<IActionResult> GetInitAddressData(Guid uniqueEntityId)
        {

            AddressViewModel model = new AddressViewModel
            {
                Address = await addressService.GetAddressByIdAsync(uniqueEntityId, 0),
                CountrySelectItems = addressDatabaseService.GetCountrySelectItemsAsync().Result,
                SuburbDataSource = addressDatabaseService.GetSuburbAsync().Result,
                IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
            };

            //if (businessProfileId > 0)
            //{
            //    model.AddressTypeSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.AddressType, businessProfileId).Result;
            //}
            //else
            //{
            //    model.AddressTypeSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.AddressType).Result;
            //}

            //if (!LoggedInUser.IsDefaultBusinessProfile)
            //{
            //model.AddressTypeSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.AddressType, businessProfileId).Result;
            //}
            //else
            //{   //get logged in user's BP items  
            //    model.AddressTypeSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.AddressType).Result;
            //}

            model.Address.CountryId = model.CountrySelectItems.FirstOrDefault(x => x.IsDefault)?.Id.ToInt() ?? 0;

            return Ok(new ResponseMessage<AddressViewModel> { Result = model });
        }


        /// <summary>
        /// CreateAddressAsync
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-address")]
        [ModelValidation]
        public async Task<IActionResult> CreateAddressAsync([FromBody] AddressModel address)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await addressService.SaveAddressAsync(0, address)
            });
        }

        /// <summary>
        /// UpdateAddressAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-address/{id:int}")]
        [ModelValidation]
        public async Task<IActionResult> UpdateAddressAsync(int id, [FromBody] AddressModel address)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await addressService.SaveAddressAsync(id, address)
            });
        }

        /// <summary>
        /// DeleteBusinessProfileAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-address-by-id/{id:int}")]
        public async Task<IActionResult> DeleteBusinessProfileAsync(int id)
        {
            return Ok(await addressService.DeleteAddressAsync(id));
        }
    }
}
