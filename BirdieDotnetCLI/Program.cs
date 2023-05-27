using System;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main()
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5069/chathub")
            .WithAutomaticReconnect()
            .Build();
       


        connection.On<string,string>("ReceiveMessage", (message, connectionId) =>
        {
            Console.WriteLine($"Received message {message} from {connectionId}");
        });

        connection.On<string>("UserDisconnected", (connectionId) => 
        {
            Console.WriteLine($"User disconnected: {connectionId}");
        });


        try 
        {
            await connection.StartAsync();
        } 
        catch (Exception ex) 
        {
            Console.WriteLine(ex.Message); 
        }

        #region ConnectionStarted

        Console.WriteLine("SignalR connection started.");

        Console.WriteLine("Enter a message and press Enter to send. Type 'exit' to quit.");
        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            await connection.InvokeAsync("SendMessage", input);
        }

        #endregion

        await connection.StopAsync();
    }
}
