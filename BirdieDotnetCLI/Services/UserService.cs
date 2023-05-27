using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdieDotnetCLI.Models;

namespace BirdieDotnetCLI.Services
{
    public class UserService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public UserService(string apiUrl)
        {
            _httpClient = new HttpClient();
            _apiUrl = apiUrl;
        }

        public async Task<bool> LoginUser(User user)
        {
            string SerializedUser = JsonConvert.SerializeObject(user);

            var RequestContent = new StringContent(SerializedUserCredentials, Encoding.UTF8, "application/json");
            var Response = await _httpClient.PostAsync("http://localhost:5069/api/user/login", RequestContent);

            //! DEBUG
            //string ResponseBody = await Response.Content.ReadAsStringAsync();

            //Console.WriteLine(ResponseBody);
            #region StatusCode

            if (Response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Welcome back, {username}!");
                return true;
            }
            else
            {
                string body = string.Empty;

                return false;

                //Console.WriteLine(b);
                //Console.WriteLine($"Username or password invalid. Status code: {Response.StatusCode}");
            }
            #endregion

        }

        private Task RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

    }
}
