using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.Api
{
   /// <summary>
   /// System rout prefix
   /// </summary>
    public static class ApplicationRoutePrefix
    {
        private const string ApplicationRoutePrefixBase = ApiRoutePrefix.RoutePrefixBase + "system-settings/";
        /// <summary>
        /// Business profile controller name
        /// </summary>
        public const string BusinessProfiles = ApplicationRoutePrefixBase + "business-profiles";
        /// <summary>
        ///Contact controller name
        /// </summary>
        public const string Contacts = ApplicationRoutePrefixBase + "contacts";
    }
}
