using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Utils
{
    public static class UserHelper
    {
        public static readonly HttpClient _httpClient = new();

        // Multi-purpose method 
        public static async Task<Dictionary<string, string>> SendAuthorizationRequest(string url, string serializedUser)
        {
            using var Http = _httpClient;

            var requestContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var Response = await Http.PostAsync(url, requestContent);

            
            var ResponseBody = await Response.Content.ReadAsStringAsync();
            var DeserializedResponseBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody) ?? new Dictionary<string, string>();
            DeserializedResponseBody.Add("status", Response.IsSuccessStatusCode.ToString());

            return DeserializedResponseBody;

        }
    }
}
