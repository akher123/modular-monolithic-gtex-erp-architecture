using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IEntityService
    {
        Task<List<SelectModel>> GetEntityListByEntityTypeAsync(int entityTypeId, int businessProfileId);
    }
}
