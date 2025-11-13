using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Crm;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Crm.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/crm/contact")]
    public class ContactController : BaseController<ContactController>
    {
        private readonly IContactService contactService;
        private readonly ICompanyService companyService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly IBusinessCategoryService businessCategoryService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IAddressDatabaseService sharedService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="companyService"></param>
        /// <param name="businessProfileService"></param>
        /// <param name="businessCategoryService"></param>
        /// <param name="customCategoryService"></param>
        /// <param name="sharedService"></param>
        public ContactController(IContactService contactService
            , ICompanyService companyService
            , IBusinessProfileService businessProfileService
            , IBusinessCategoryService businessCategoryService
            , ICustomCategoryService customCategoryService
            , IAddressDatabaseService sharedService)
        {
            this.contactService = contactService;
            this.companyService = companyService;
            this.businessProfileService = businessProfileService;
            this.customCategoryService = customCategoryService;
            this.sharedService = sharedService;
            this.businessCategoryService = businessCategoryService;
        }
        /// <summary>
        /// Get Contacts Async
        /// </summary>
        /// <param name="loadOptions">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contacts")]
        [ActionAuthorize(ApplicationPermission.Contact.ShowContactList)]
        public async Task<IActionResult> GetContactsAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult>
            {
                Result = await contactService.GetContactListAsync(loadOptions)
            });
        }
        /// <summary>
        /// GetEmailServersAsync
        /// </summary>
        /// <param name="businessProfileId">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contacts-by-bp/{businessProfileId:int}")]
        [ActionAuthorize(ApplicationPermission.Contact.ViewContactDetails)]
        public async Task<IActionResult> GetContactSelectItemsByBusinessProfileId(int businessProfileId)
        {
            return Ok(new ResponseMessage<List<SelectModel>>
            {
                Result = await contactService.GetContactSelectItemsByBusinessProfileIdAsync(businessProfileId)
            });
        }

        /// <summary>
        /// get company by business profile id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get-contact-company-list")]
        public async Task<IActionResult> GetCompanyByBusinessProfileId([FromBody] List<int> ids)
        {
            return Ok(new ResponseMessage<List<SelectModel>> { Result = await companyService.GetCompanySelectItemsByBPIdAsync(ids) });
        }
        /// <summary>
        /// Get Contact Details Tab Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contact-details-tab")]
        [ActionAuthorize(ApplicationPermission.Contact.ViewContactDetails)]
        public async Task<IActionResult> GetContactDetailsTabAsync(int id)
        {
            return Ok(new ResponseMessage<TabPageViewModel>
            {
                Result = await Task.Run(() => new TabPageViewModel
                {
                    TabItems = contactService.GetContactDetailsTabs(id),
                    EntityType = ApplicationEntityType.Contact
                })
            });
        }
        /// <summary>
        /// Get Contact Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contact")]
        [ActionAuthorize(ApplicationPermission.Contact.ViewContactDetails)]
        public async Task<IActionResult> GetContactAsync(int id)
        {
            return Ok(new ResponseMessage<ContactViewModel>
            {
                Result = new ContactViewModel
                {



                    ContactModel = await contactService.GetContactModelByIdAsync(id),
                    EmptyContactModel = contactService.GetContactModelByIdAsync(0).Result,

                    BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                    TimezoneSelectItems = sharedService.GetTimeZoneSelectItemsAsync().Result,
                    //CompanySelectItems = companyService.GetCompanySelectItemsAsync().Result,



                    PreferredPhoneTypeSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.PreferredPhoneType),

                    EntityType = ApplicationEntityType.Contact,
                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bps"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-contact-select-items-by-bpids")]
        public async Task<IActionResult> GetContactSelectItemsByBpIdsAsync(int[] bps)
        {

            ResponseMessage<ContactViewModel> response = new ResponseMessage<ContactViewModel>
            {
                Result = new ContactViewModel()
            };

            if (bps.Length == 1)
            {
                response.Result.ImTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.IMType, bps[0]);
                response.Result.TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps[0]).Result;
                response.Result.PositionSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PositionType, bps[0]).Result;

                response.Result.SkillsSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.SkillType, bps[0]).Result;
                response.Result.GenderSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.GenderType, bps[0]).Result;
                response.Result.PreferredContactMethodSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PreferredContactMethod, bps[0]).Result;
            }
            else if (bps.Length > 1)
            {
                response.Result.ImTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.IMType, bps);
                response.Result.TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, bps).Result;
                response.Result.PositionSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PositionType, bps).Result;

                response.Result.SkillsSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.SkillType, bps).Result;
                response.Result.GenderSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.GenderType, bps).Result;
                response.Result.PreferredContactMethodSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PreferredContactMethod, bps).Result;
            }
            else
            {
                //load custom category by logged in user's business profile id 
                response.Result.ImTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.IMType);
                response.Result.TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType).Result;
                response.Result.PositionSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PositionType).Result;

                response.Result.SkillsSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.SkillType).Result;
                response.Result.GenderSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.GenderType).Result;
                response.Result.PreferredContactMethodSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PreferredContactMethod).Result;
            }

            return Ok(response);
        }

        /// <summary>
        /// Create contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-contact")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Contact.CreateContact)]
        public async Task<IActionResult> CreateContact([FromBody] ContactModel contact)
        {
            ResponseMessage<int> response = new ResponseMessage<int>
            {
                Result = await contactService.SaveContactDetailsAsync(contact)
            };
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-contact")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Contact.UpdateContact)]
        public async Task<IActionResult> UpdateContact([FromBody] ContactModel contact)
        {
            ResponseMessage<int> response = new ResponseMessage<int>
            {
                Result = await contactService.SaveContactDetailsAsync(contact)
            };
            return Ok(response);
        }

        /// <summary>
        /// delete contacts by id list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-contact/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Contact.DeleteContact)]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            return Ok(await contactService.DeleteContactAsync(id));
        }

        /// <summary>
        /// delete contacts by id list
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-contacts")]
        [ActionAuthorize(ApplicationPermission.Contact.DeleteContact)]
        public async Task<IActionResult> DeleteContactsAsync([FromBody]List<int> ids)
        {
            return Ok(await contactService.DeleteContactAsync(ids));
        }
    }
}
