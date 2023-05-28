using System;
using BirdieDotnetCLI.Models;
using BirdieDotnetCLI.Services;
using BirdieDotnetCLI.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;


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
        
        //TODO          add proper validation checks

        int UserChoice = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Username: ");
        string Username = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Password: ");
        string Password = Console.ReadLine() ?? string.Empty;

        // -------------------------------------------------------- //
        #endregion

        User user = new User(Username, Password);
        bool result;

        switch (UserChoice)
        {
            case 1:
                result = UserService.AuthorizeUser(ref user, atEndpoint: "/login"); //TODO needs fix UserService
                ShowConversations();

                break;
            case 2:
                 result = UserService.AuthorizeUser(ref user, atEndpoint: "/register"); //TODO needs fix UserService

                break;
            default:
                Console.WriteLine("Invalid choice. Retry");
                break;
        }
    }
}    

