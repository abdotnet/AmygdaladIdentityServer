using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amygdalab.Web.Extensions.Identity
{
    public static class Config
    {
        public static List<Client> GetClient(IConfiguration configuration)
        {
            List<Client> Clients = new List<Client>
    {
           // resource owner password grant client
                new Client
                {
                    ClientId = "ro.angular",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientName="Angular client",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1"
                    },
                    //Allow requesting refresh tokens for long lived API access
                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    UpdateAccessTokenClaimsOnRefresh=true,
                    AccessTokenLifetime=Convert.ToInt32(configuration["JwtSetting:AccessTokenLifetime"]),  // 10 days
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime=Convert.ToInt32(configuration["JwtSetting:SlidingRefreshTokenLifetime"]) // 20 
                }
        };

            return Clients;
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
       {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"Worker"}
            }
        };
        }
        public static List<ApiResource> ApiResources = new List<ApiResource>
    {
        new ApiResource
        {
            Name = "api1",
            DisplayName = "Amygdalab Demo API",
            UserClaims= new List<string> {"Admin","Worker"},
            Scopes = new List<string>
            {
                "write",
                "read"
            }
        }
    };

        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
    {
        new ApiScope("openid"),
        new ApiScope("profile"),
        new ApiScope("email"),
        new ApiScope("read"),
        new ApiScope("write"),
        new ApiScope("api1")
    };
    }
}
