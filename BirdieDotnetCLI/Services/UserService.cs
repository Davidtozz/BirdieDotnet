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
            string SerializedUser = JsonConvert.SerializeObject(user);
            return await SendUserRequest(endpoint: $"{apiUrl}/login", SerializedUser); 
            
        }

        public static async Task<bool> RegisterUser(User user)
        {
            string SerializedUser = JsonConvert.SerializeObject(user); 
            return await SendUserRequest($"{apiUrl}/new", SerializedUser);
        }

        // Multi-purpose method 
        private static async Task<bool> SendUserRequest(string endpoint, string serializedUser)
        {
            var requestContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var Response = await _httpClient.PostAsync(endpoint, requestContent);

            return Response.IsSuccessStatusCode;
        }

    }
}
