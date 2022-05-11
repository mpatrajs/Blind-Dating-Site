using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BDate.Hubs {
    public class ChatHub : Hub {

        public async Task SendMessage(string user, string message, string room, bool join) {
            if (join) {
                await JoinRoom(room).ConfigureAwait(false);
                await Clients.Group(room).SendAsync("ReceiveMessage", user, " joined to " + room).ConfigureAwait(true);

            } else {
                await Clients.Group(room).SendAsync("ReceiveMessage", user, message).ConfigureAwait(true);

            }
        }

        public async Task SendToUser(string user, string receiverConnectionId, string message) {
            if (!String.IsNullOrEmpty(message)) {
                //Returning to view our send messege
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
                //Returning messege which was written by partner
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", user, message);
            }
        }
        public Task JoinRoom(string roomName) {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName) {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
        public string GetConnectionId() => Context.ConnectionId;
    }
}
