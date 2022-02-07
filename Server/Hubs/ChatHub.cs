using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToAll (string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);   
        }
        public async Task SendMessageToOne (string userId, string message)
        {

            await Clients.User(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
