using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("webapi", "Full access to API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("webapi", "WebApi") {Scopes = {"webapi"}}
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "a434fca7-d0d5-45fd-b9e0-aa5c7d9a441f",
                    ClientName = "Swagger UI for WebApi",
                    ClientSecrets = {new Secret("d0e4dec2-5544-4872-9015-cdb864e72dd8".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:7213/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:7213"},
                    AllowedScopes = {"webapi"}
                }
            };
    }
}
