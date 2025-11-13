using Softcode.GTex.ApplicationService.Crm;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Web.Api.ApplicationService
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityService : BaseEntityService
    {
        private readonly ICompanyService companyService;
        private readonly IContactService contactService;
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationServiceBuilder"></param>
        /// <param name="companyService"></param>
        /// <param name="contactService"></param>
        public EntityService(IApplicationServiceBuilder applicationServiceBuilder
            , ICompanyService companyService
            , IContactService contactService) : base(applicationServiceBuilder)
        {
            this.companyService = companyService;
            this.contactService = contactService;
        }

 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override async  Task<List<SelectModel>> GetEntityListByEntityTypeAsync(int entityTypeId, int businessProfileId)
        {

            if (entityTypeId == ApplicationEntityType.Company)
            {
                return await companyService.GetCompanySelectItemsByBPIdAsync(businessProfileId);
            }
            else if (entityTypeId == ApplicationEntityType.Contact)
            {
                return await contactService.GetContactSelectItemsByBPIdAndContactTypeAsync(businessProfileId, ApplicationContactType.Contact);
            }
            else if (entityTypeId == ApplicationEntityType.Employee)
            {
                return await contactService.GetContactSelectItemsByBPIdAndContactTypeAsync(businessProfileId, ApplicationContactType.Employee);
            }

            return new List<SelectModel>();
        }

       
    }
}
