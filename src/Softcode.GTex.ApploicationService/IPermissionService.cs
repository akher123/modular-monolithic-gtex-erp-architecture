using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IPermissionService
    {
        void Initialize(ILoggedInUser loggedInUserService, IApplicationCacheService applicationCacheService);
        List<int> DefaultBusinessProfileRights { get; }

        bool IsValid(int rightId);
    }
}
