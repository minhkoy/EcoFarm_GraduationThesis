using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Common.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            var user1 = Context.UserIdentifier;
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendNotification(string user, string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", user, message);
        }


    }
}
