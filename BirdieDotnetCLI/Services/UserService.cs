using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdieDotnetCLI.Models;

namespace BirdieDotnetCLI.Services
{
    public static class UserService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5069/api/user";

        public static async Task<bool> LoginUser(User user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var response = await SendUserRequest($"{apiUrl}/login", serializedUser).Result;
            
            user.AuthorizationToken = response["token"];
            
            return true;
        }

        public static async Task<bool> RegisterUser(User user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var response = await SendUserRequest($"{apiUrl}/new", serializedUser).Result;

            user.AuthorizationToken = response["token"];

            return true;
        }

        // Multi-purpose method 
        private static async Task<dynamic> SendUserRequest(string endpoint, string serializedUser)
        {
            var requestContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var Response = await _httpClient.PostAsync(endpoint, requestContent);

            if (Response.IsSuccessStatusCode)
            {
                Console.WriteLine("Login successful!");
                var ResponseBody = await Response.Content.ReadAsStringAsync();
                var DeserializedResponseBody = JsonConvert.DeserializeObject<Dictionary<string,string>>(ResponseBody);
                DeserializedResponseBody.Add("success",Response.IsSuccessStatusCode.ToString());
               
                return DeserializedResponseBody;
            }
            else 
            {
                return null;
            }
            
        }

    }
}
