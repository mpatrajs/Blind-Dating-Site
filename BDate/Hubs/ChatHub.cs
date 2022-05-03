using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BDate.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }

        public async Task SendToUser(string user, string receiverConnectionId, string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, message);
            }
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
