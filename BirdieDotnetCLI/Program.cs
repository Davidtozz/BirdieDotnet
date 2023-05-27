using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using BirdieDotnetCLI.Models;
using BirdieDotnetCLI.Services;
using BirdieDotnetCLI.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;

internal static class Program
{
    public static async Task Main()
    {
        SignalRService signalRConnection = new("http://localhost:5069/chathub");
        
        /* Menu AuthenticationMenu = new Menu()
                                     .withTitle("Welcome to BirdieDotnet!")
                                     .withOptions("Login", "Register"); */
        #region WelcomePage

        Console.WriteLine("Welcome to BirdieDotnet! \n1. Login\n2. Register");
        
        int UserChoice = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Username: ");
        string Username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Password: ");
        string Password = Console.ReadLine() ?? string.Empty;

        #endregion

        User user = new User(Username, Password);
        bool result;

        switch (UserChoice)
        {
            /*
            case 1:
                result = UserService.LoginUser(new User(Username, Password)).Result;

                if (result != null)

                    Console.WriteLine($"Login success! Welcome back {Username}. \n (debug) TOKEN: {result}");

                else
                    Console.WriteLine("Login failed :(");
                break;
            case 2:
                result = await UserService.RegisterUser(new User(Username, Password));
                if (result.Success != true)
                {
                    Console.WriteLine("Registration failed :(");
                       
                }
                else
                {
                    Console.WriteLine($"Registration success! Welcome aboard {Username}");
                }
                break;
                */

            case 1:
                result = UserService.LoginUser(user).Result; //TODO needs fix UserService

                if (result)
                {
                    Console.WriteLine($"Logged in!\n TOKEN: {user.AuthorizationToken}");
                }
                else 
                {
                    Console.WriteLine("Login unsuccessful.");
                }

                break;
            case 2:
                 result = UserService.RegisterUser(user).Result; //TODO needs fix UserService

                if (result)
                {
                    Console.WriteLine($"Logged in!\n TOKEN: {user.AuthorizationToken}");
                }
                else
                {
                    Console.WriteLine("Login unsuccessful.");
                }

                break;
            default:
                Console.WriteLine("Invalid choice. Retry");
                break;
        }
    }
}    

