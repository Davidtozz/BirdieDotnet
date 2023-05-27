using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using BirdieDotnetCLI.Models;
using BirdieDotnetCLI.Services;
using BirdieDotnetCLI.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

internal static class Program
{
    public static async Task Main()
    {
        //SignalRService signalRConnection = new("http://localhost:5069/chathub");

       /* Menu AuthenticationMenu = new Menu()
                                    .withTitle("Welcome to BirdieDotnet!")
                                    .withOptions("Login", "Register"); */

        Console.WriteLine("Welcome to BirdieDotnet! \n1. Login\n2. Register");


        
        int UserChoice = int.Parse(Console.ReadLine() ?? "0");

        
       
       

        Console.WriteLine("Username: ");
        string Username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Password: ");
        string Password = Console.ReadLine() ?? string.Empty;

        bool result;

        switch (UserChoice)
        {
            case 1:
                result = await UserService.LoginUser(new User(Username, Password));

                if (result != true)
                    Console.WriteLine("Login failed :(");
                else 
                    Console.WriteLine($"Login success! Welcome back {Username}");
                break;
            case 2:
                result = await UserService.RegisterUser(new User(Username, Password));
                if (result != true)
                {
                    Console.WriteLine("Registration failed :(");
                       
                }
                else
                {
                    Console.WriteLine($"Registration success! Welcome aboard {Username}");
                }
                break;

            default:
                Console.WriteLine("Invalid choice. Retry");
                break;
        }
    }
}    

