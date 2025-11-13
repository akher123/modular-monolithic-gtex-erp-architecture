using System.Collections.Generic;

namespace Softcode.GTex
{
    public interface ILoggedInUserService
    {
        ILoggedInUser LoggedInUser { get; }

        bool IsApplicationServiceUser { get; set; }

        void ReloadLoggedInUserData();
        


    }
}
