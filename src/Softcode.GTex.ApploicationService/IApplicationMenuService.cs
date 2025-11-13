using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IApplicationMenuService
    {
        Task<List<ApplicationMenuModel>> GetApplicationMenuAsync();
        Task<ApplicationHeaderModel> GetApplicationHeaderAsync();
        Task<int> CreateApplicationMenuAsync(ApplicationMenuModel model);
        Task<int> UpdateApplicationMenuAsync(int id, ApplicationMenuModel model);
        Task<List<TreeModel>> ApplicationMenuTreeListAsync();
    }
}
