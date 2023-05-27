using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using BirdieDotnetCLI;
using BirdieDotnetCLI.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

internal static class Program
{
    public static async Task Main()
    {
        SignalRConnectionHandler signalRConnection = new("http://localhost:5069/chathub");
        
       /*
        
        await signalRConnection.StartConnection
        Console.WriteLine("SignalR connection established successfully.");
        Console.WriteLine("Type a message to send, or 'exit' to exit.");
        await signalRConnection.SendUserInput();

        */
        
        Console.WriteLine("Welcome to BirdieDotnet! \n1. Login\n2. Register");
        int UserChoice = int.Parse(Console.ReadLine() ?? "0");

        while (UserChoice < 1 || UserChoice > 2)
        {
            Console.WriteLine("Invalid input. Retry");
            UserChoice = int.Parse(Console.ReadLine() ?? "0");
        }

        switch (UserChoice)
        {
            case 1:

                Console.WriteLine("Username: ");
                string Username = Console.ReadLine() ?? "";

                Console.WriteLine("Password: ");
                string Password = Console.ReadLine() ?? "";

                await LoginUser(Username,Password);
                break;

            case 2:
                await RegisterUser();
                break;
        }
    }

    private static Task RegisterUser()
    {
        throw new NotImplementedException();
    }

    public static async Task LoginUser(string username, string psw)
    {
        using var client = new HttpClient();

        string SerializedUserCredentials = JsonConvert.SerializeObject(new User(username, psw));

        Console.WriteLine(SerializedUserCredentials);

        var RequestContent = new StringContent(SerializedUserCredentials, Encoding.UTF8, "application/json");
        var Response = await client.PostAsync("http://localhost:5069/api/user/login", RequestContent);

        //! DEBUG
        //string ResponseBody = await Response.Content.ReadAsStringAsync();

        //Console.WriteLine(ResponseBody);
        
        if (Response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Welcome back, {username}!");
        }
        else 
        {
            string body = string.Empty;

            //Console.WriteLine(b);
            //Console.WriteLine($"Username or password invalid. Status code: {Response.StatusCode}");
        }
    }
} 