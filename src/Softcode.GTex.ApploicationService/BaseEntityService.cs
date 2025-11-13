using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public abstract class BaseEntityService : BaseService<BaseEntityService>, IEntityService
    {
        public BaseEntityService(IApplicationServiceBuilder applicationServiceBuilder) 
            : base(applicationServiceBuilder)
        {
        }

        public abstract Task<List<SelectModel>> GetEntityListByEntityTypeAsync(int entityTypeId, int businessProfileId);
    }
}
