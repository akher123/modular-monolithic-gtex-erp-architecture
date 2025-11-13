using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex
{
    public class PortalConfig
    {
        public static string Authority
        {
            get { return ConfigurationManager.AppSettings["Authority"]; }
        }
        public static string IssuerUri
        {
            get { return ConfigurationManager.AppSettings["IssuerUri"]; }
        }

        public static string PublicOrigin
        {
            get { return ConfigurationManager.AppSettings["PublicOrigin"]; }
        }

        public static bool IsMemoryUse
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsMemoryUse"]); }
        }

        public static string BaseImageUploadFolder
        {
            get { return ConfigurationManager.AppSettings["BaseImageUploadFolder"]; }
        }
    }
}
