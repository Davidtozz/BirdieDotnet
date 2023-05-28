using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Utils
{
    public static class UserHelper
    {
        public static readonly HttpClient _httpClient = new();

        // Multi-purpose method 
        public static async Task<Dictionary<string, string>> SendAuthorizationRequest(string url, string data)
        {
            using var Http = _httpClient;

            var requestContent = new StringContent(data, Encoding.UTF8, "application/json");
            var Response = await Http.PostAsync(url, requestContent);
            var ResponseBody = await Response.Content.ReadAsStringAsync();
            var DeserializedResponseBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody) ?? new Dictionary<string, string>();
            DeserializedResponseBody.Add("status", Response.IsSuccessStatusCode.ToString());

            return DeserializedResponseBody;
        }

        public static string SendAuthorizationRequest(string url, string data, string token)
        {
            using var httpClient = _httpClient;

            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpRequestMessage request = new(HttpMethod.Post, url);

            request.Headers.Add("Authorization", $"Bearer {token}"); // Authorization Header
            request.Content = new StringContent(data, Encoding.UTF8, "application/json"); // Content-Type Header

            return Task.Run(async () => await httpClient.Send(request).Content.ReadAsStringAsync()).Result;
                            

            /* var Response = await HttpClient.PostAsync(url, requestContent);
            var ResponseBody = await Response.Content.ReadAsStringAsync();
            var DeserializedResponseBody = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponseBody) ?? new Dictionary<string, string>();
            DeserializedResponseBody.Add("status", Response.IsSuccessStatusCode.ToString());

            return DeserializedResponseBody;*/



        }
    }
}
