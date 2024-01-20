using Microsoft.AspNetCore.SignalR;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Common.Hubs
{
    public class UserConnectionHub : Hub
    {
        public static int OnlineUsers = 0;
        public override Task OnConnectedAsync()
        {
            OnlineUsers++;
            Clients.All.SendAsync(nameof(EventType.UserOnline), OnlineUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            OnlineUsers--;
            Clients.All.SendAsync(nameof(EventType.UserOnline), OnlineUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
