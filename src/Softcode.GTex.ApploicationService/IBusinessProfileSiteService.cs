using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IBusinessProfileSiteService
    {
        Task<List<BusinessProfileSiteSelectModel>> GetBusinessProfileSiteListAsync(int businessProfileId);
    }
}
