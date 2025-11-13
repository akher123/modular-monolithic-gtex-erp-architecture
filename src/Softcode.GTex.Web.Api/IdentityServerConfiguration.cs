using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace Softcode.GTex.Web.Api
{
    public class IdentityServerConfiguration
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Softcode-GTex-API", "Softcode GTE API Service")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            IdentityServerConfig adminConfiguration = ApplicationConfigurations.IdentityServerConfigs.Where(x => x.ClientId == "gtex-angular-client").FirstOrDefault();

            // "Softcode-GTex-API"
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "gtex-angular-client",
                    ClientName = "GTex Angular Client",
                    ClientUri = adminConfiguration.ClientUri,
                    AccessTokenLifetime = adminConfiguration.AccessTokenLifetime,
                    IdentityTokenLifetime = adminConfiguration.IdentityTokenLifetime,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = adminConfiguration.RequireConsent,
                    RedirectUris =adminConfiguration.RedirectUris,
                    PostLogoutRedirectUris = adminConfiguration.PostLogoutRedirectUris,
                    AllowedCorsOrigins = adminConfiguration.AllowedCorsOrigins,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "Softcode-GTex-API"
                    }
                }
            };
        }
    }
}
