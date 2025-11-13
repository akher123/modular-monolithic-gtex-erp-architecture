using Softcode.GTex.ApploicationService.Models;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IApplicationPageService
    {
        Task<ApplicationListPageModel> GetApplicationListPageByNameAsync(string name);
        Task<ApplicationListPageModel> GetApplicationListPageByRoutingUrlAsync(string routingUrl);
        Task<ApplicationDetailPageModel> GetApplicationDetailPageByNameAsync(string name);
        Task<ApplicationDetailPageModel> GetApplicationDetailPageByRoutingUrlAsync(string routingUrl);
        Task<ApplicationPageModel> GetApplicationListPageByIdAsync(int id);
        Task<ApplicationPageModel> GetApplicationDetailPageByIdAsync(int id);

    }
}
