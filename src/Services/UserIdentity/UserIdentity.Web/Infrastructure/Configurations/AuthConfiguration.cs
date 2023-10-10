using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace UserIdentity.Web.Infrastructure.Configurations;

public class AuthConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("LotLynxApi", "LotLynx API")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource> 
        {
            new ApiResource("LotLynxApi","LotLynx API", new[]
                {
                    JwtClaimTypes.Name
                })
                {
                    Scopes = { "LotLynxApi" }
                }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client> 
        { 
            new Client
            {
                ClientId = "test",
                ClientName = "testUI",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowAccessTokensViaBrowser = true,

                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "LotLynxApi"
                }
            },
        };
}
