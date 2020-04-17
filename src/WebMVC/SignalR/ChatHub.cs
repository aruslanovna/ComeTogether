
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebMVC
{
    public class ChatHub : Hub
    {
        
        string groupname = "cats";
        public async Task Enter(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                await Clients.Caller.SendAsync("Notify", "Для входа в чат введите логин");
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupname);
                await Clients.Group(groupname).SendAsync("Notify", $"{username} вошел в чат");
            }
        }
        public async Task Send(string message, string username)
        {
            await Clients.Group(groupname).SendAsync("Receive", message, username);
        }
    }

}



