using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Crm.Models;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Crm;
using Softcode.GTex.ApplicationService.Crm.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Crm.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/crm/company")]
    public class CompanyController : BaseController<CompanyController>
    {
        private readonly ICompanyService companyService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly IAddressDatabaseService addressDatabaseService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IBusinessCategoryService businessCategoryService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyService"></param>
        /// <param name="businessProfileService"></param>
        /// <param name="addressDatabaseService"></param>
        /// <param name="customCategoryService"></param>
        /// <param name="businessCategoryService"></param>
        public CompanyController(ICompanyService companyService
            , IBusinessProfileService businessProfileService
            , IAddressDatabaseService addressDatabaseService
            , ICustomCategoryService customCategoryService
            , IBusinessCategoryService businessCategoryService
            )
        {
            this.companyService = companyService;
            this.businessProfileService = businessProfileService;
            this.addressDatabaseService = addressDatabaseService;
            this.customCategoryService = customCategoryService;
            this.businessCategoryService = businessCategoryService;
        }

        /// <summary>
        /// Get all company data
        /// </summary>
        /// <param name="loadOptions">Dev express data model</param>
        /// <returns>Devexpress data grid model</returns>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Product created</response>
        /// <response code="400">Product has missing/invalid values</response>
        /// <response code="500">Oops! Can't create your product right now</response>
        [HttpGet]
        [Route("get-companies")]
        [ActionAuthorize(ApplicationPermission.Company.ShowCompanyList)]
        public async Task<IActionResult> GetCompaniesAsync(DataSourceLoadOptions loadOptions)
        {
            var res = LoggedInUser.ContactBusinessProfileIds;

            return Ok(new ResponseMessage<LoadResult> { Result = await companyService.GetCompanyListAsync(loadOptions) });
        }
        /// <summary>
        /// Get Company Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-company")]
        [ActionAuthorize(ApplicationPermission.Company.ViewCompanyDetails)]
        public async Task<IActionResult> GetCompanyAsync(int id)
        {
            ResponseMessage<CompanyViewModel> response = new ResponseMessage<CompanyViewModel>
            {
                Result = new CompanyViewModel
                {
                    EntityType = ApplicationEntityType.Company,
                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile,
                    EmptyCompany= companyService.GetCompanyByIdAsync(0).Result
                }
            };

            response.Result.Company = await companyService.GetCompanyByIdAsync(id);
            response.Result.BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result;
            response.Result.CounrtySelectItems = addressDatabaseService.GetCountrySelectItemsAsync().Result;
            response.Result.RelationshipTypeSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.RelationshipType);          

            return Ok(response);
        }
         

        /// <summary>
        /// Get Company Select items Async
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-company-select-items/{businessProfileId}")]
        
        public async Task<IActionResult> GetCompanySelectItemsAsync([Range(1, int.MaxValue)]int businessProfileId)
        {
            return Ok(new ResponseMessage<CompanyViewModel>
            {
                Result = new CompanyViewModel
                {
                    OrganisationTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.OrganisationType, businessProfileId),
                    PreferredContactMethodSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PreferredContactMethod, businessProfileId).Result,
                    IndustrySelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.IndustryType, businessProfileId).Result,
                    RatingSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.RatingType, businessProfileId).Result
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-company")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Company.CreateCompany)]
        public async Task<IActionResult> CreateCompanyAsync([FromBody] CompanyModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await companyService.SaveCompanyDetailsAsync(0, model)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-company/{id:int}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Company.UpdateCompany)]
        public async Task<IActionResult> UpdateCompanyAsync(int id, [FromBody] CompanyModel model)
        {
            return Ok(new ResponseMessage<int>
            {
                Result = await companyService.SaveCompanyDetailsAsync(id, model)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-company/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Company.DeleteCompany)]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            return Ok(await companyService.DeleteCompanyAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-companies")]
        [ActionAuthorize(ApplicationPermission.Company.DeleteCompany)]
        public async Task<IActionResult> DeleteCompaniesAsync([FromBody] List<int> ids)
        {
            return Ok(await companyService.DeleteCompaniesAsync(ids));
        }

        /// <summary>
        /// Get Contact Details Tab Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-company-details-tab/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Company.ViewCompanyDetails)]
        public async Task<IActionResult> GetCompanyDetailsTabAsync(int id)
        {
            return Ok(new ResponseMessage<TabPageViewModel>
            {
                Result = await Task.Run(() => new TabPageViewModel
                {
                    TabItems = companyService.GetCompanyDetailsTabs(id),
                    EntityType = ApplicationEntityType.Company
                })
            });
        }
    }
}
