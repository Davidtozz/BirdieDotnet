using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdieDotnetCLI.Models;
using BirdieDotnetCLI.Services;

namespace BirdieDotnetCLI.Services
{
    public class ChatService
    {
        
        SignalRService signalRService;

        public ChatService(SignalRService signalRService)
        {
            this.signalRService = signalRService;
        }
        
        public async Task ConnectToChatHub() 
        {
            await signalRService.StartConnection();
        }

        public async Task DisconnectToChatHub()
        {
            throw new NotImplementedException();
        }

        public async Task SendMessage()
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> GetChatHistory()
        {
            throw new NotImplementedException();
        }

    }
}