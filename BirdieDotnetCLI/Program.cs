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

        var chatService = new ChatService(HubUrl: "http://localhost:5069/chathub");
        
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

                chatService.currentUser = user;


                result = UserService.AuthorizeUser(ref user, atEndpoint: "/login"); //TODO needs fix UserService
                user.ConnectionId = await chatService.StartConnection();

                Console.WriteLine($"{user.Name} : {user.ConnectionId}\n----------------------------------");

                while(true) 
                {
                    Console.Write($"\u001b[1;36m{user.Name}:\u001b[0m ");
                    
                    var text = Console.ReadLine();
                    
                    await chatService.SendMessage(text, user);
                    
                }


                break;
            case 2:
                 result = UserService.AuthorizeUser(ref user, atEndpoint: "/register"); //TODO needs fix UserService
                await chatService.StartConnection();
                break;
            default:
                Console.WriteLine("Invalid choice. Retry");
                break;
        }
    }
}    

