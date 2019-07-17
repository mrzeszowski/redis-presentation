using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RedisPresentation.Hubs
{
    public class ExampleHub : Hub
    {
        public async Task DoAction(string user, string action)
        {
            await Clients.All.SendAsync("DoAction", user, action);
        }
    }
}
