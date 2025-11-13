using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Softcode.GTex.ApploicationService
{
    public class BusinessProfileSiteService : BaseService<BusinessProfileSite>, IBusinessProfileSiteService
    {
        private readonly IRepository<BusinessProfileSite> businessProfileSiteRepository;

        public BusinessProfileSiteService(IRepository<BusinessProfileSite> businessProfileSiteRepository,
             IApplicationServiceBuilder applicationServiceBuilder) : base(applicationServiceBuilder)
        {
            this.businessProfileSiteRepository = businessProfileSiteRepository;
        }

        public async Task<List<BusinessProfileSiteSelectModel>> GetBusinessProfileSiteListAsync(int businessProfileId)
        {


            return await Task.Run(() => this.businessProfileSiteRepository.Where(c => c.IsActive
                       && c.BusinessProfileId == businessProfileId)
                    //.Include(t => t.Employee)
                    .Include(t => t.Address)
                    .ThenInclude(t => t.State)
                    .ToList()
                    .Select(x => new BusinessProfileSiteSelectModel
                    {

                        Id = x.Id,
                        Name = x.SiteName,
                        Address = x.AddressId > 0 ?
                               x.Address.Street
                              + (string.IsNullOrEmpty(x.Address.Suburb) ? "" : ", " + x.Address.Suburb)
                              + (x.Address.State == null ? "" : ", " + x.Address.State.StateName)
                              + (string.IsNullOrEmpty(x.Address.PostCode) ? "" : " " + x.Address.PostCode)
                              : ""
                    }).ToList<BusinessProfileSiteSelectModel>()

                    );
        }

        
    }
}
