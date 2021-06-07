using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.IdentityServices
{
    public class ClaimUpdate
    {
        private readonly IConfiguration _configuration;

        public ClaimUpdate(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Dictionary<string, string>> RefreshToken(string refreshToken)
        {
            using (var client = new HttpClient())
            {
                // var url = Startup.StaticConfig.GetSection("AppSettings").GetSection("ApiUrl").Value + "/connect/token";
                //var url = "https://localhost:44339/connect/token";
                var url = string.Concat(_configuration["AppSettings:Authority"], "connect/token");
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StringContent(refreshToken), "refresh_token");
                    //content.Add(new StringContent("openid offline_access"), "scope");
                    content.Add(new StringContent("ro.angular"), "client_id");
                    content.Add(new StringContent("refresh_token"), "grant_type");
                    content.Add(new StringContent("secret"), "client_secret");
                    var result = await client.PostAsync(url, content);
                    var access_token = "";
                    var refresh_token = "";
                    JObject objects = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                    foreach (var kv in objects)
                    {
                        if (kv.Key == "access_token")
                        {
                            access_token = kv.Value.ToString();
                        }
                        if (kv.Key == "refresh_token")
                        {
                            refresh_token = kv.Value.ToString();
                        }
                    }
                    Dictionary<string, string> token = new Dictionary<string, string>();
                    token.Add("accessToken", access_token);
                    token.Add("refreshToken", refresh_token);
                    return token;
                }
            }
        }
    }
}
