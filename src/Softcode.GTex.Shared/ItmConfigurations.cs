using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex
{
    public static class ItmConfigurations
    {
        //public static IConfiguration Configuration { get; }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
         .AddEnvironmentVariables()
         .Build();

        public static string ApplicationUrl
        {
            get
            {
                return Configuration["ApplicationUrl"];
            }
        }

        public static string[] ApplicationAllowedUrls
        {
            get
            {
                return Configuration.GetSection("ApplicationAllowedUrls").Get<string[]>();
            }
        }

        public static string ApplicationConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("ApplicationConnection");
            }
        }

        public static string FileStorageConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("FileStorageConnection");
            }
        }

        public static string HangfireConnectionString
        {
            get
            {
                return Configuration.GetConnectionString("HangfireConnection");
            }
        }

        public static string TokenAuthority
        {
            get
            {
                return Configuration["TokenAuthority"];
            }
        }

        public static string TokenAudience
        {
            get
            {
                return Configuration["TokenAudience"];
            }
        }

        public static List<IdentityServerConfig> IdentityServerConfigs
        {
            get
            {
                return Configuration.GetSection("IdentityServerClients").Get<List<IdentityServerConfig>>();
            }
        }

        public static List<ClientApplicationConfig> ClientApplicationConfigs
        {
            get
            {
                return Configuration.GetSection("ClientApplicationConfig").Get<List<ClientApplicationConfig>>();
            }
        }

        public static string LoginTitle
        {
            get
            {
                return Configuration["LoginTitle"];
            }
        }

        public static int DefaultCacheTimeout
        {
            get
            {
                return 1;
            }
        }

    }
}
