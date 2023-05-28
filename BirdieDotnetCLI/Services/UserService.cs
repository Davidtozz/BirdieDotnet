using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdieDotnetCLI.Models;
using BirdieDotnetCLI.Utils;
using System.Net.Http;

namespace BirdieDotnetCLI.Services
{
    public static class UserService
    {
        public static bool AuthorizeUser(ref User user, string atEndpoint)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var response = Task.Run(async () => await UserHelper.SendAuthorizationRequest($"http://localhost:5069/api/user{atEndpoint}", serializedUser)).Result;

            if (bool.Parse(response["status"]))
            {
                user.AuthorizationToken = response["token"];
                Console.WriteLine("Operation success!");
                return true;
            }
            else
            {
                Console.WriteLine("Something went wrong while authenticating :(");
                return false; 
            }
            
        }

        public static void ShowConversations(User user)
        {
            throw new NotImplementedException();    
        }
       

    }

    

}
