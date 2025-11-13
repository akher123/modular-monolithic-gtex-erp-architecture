using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface ICostCentreService
    {
        Task<List<SelectModel>> GetCostCentreSelectItemsAsync(int businessProfileId);
    }
}