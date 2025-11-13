using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IDepartmentService
    {
        Task<List<SelectModel>> GetBusinessProfileDepartmentSelectItemsAsync(int businessProfileId);
    }
}