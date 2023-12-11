using EcoFarm.Api.Hubs.Requests;
using EcoFarm.Domain.Common.Values.Constants;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TokenHandler.Interfaces;

namespace EcoFarm.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ChatHub(IAuthService authService, IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public override Task OnConnectedAsync()
        {
            var userId = _authService.GetAccountEntityId();
            var identity = new ClaimsIdentity(userId);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            //identity.AddClaim(new Claim(ClaimTypes.Name, _authService.GetFullname()));
            //var principal = new ClaimsPrincipal(identity);
            Context.User.AddIdentity(identity);
            Console.WriteLine("Token: " + httpContextAccessor.HttpContext.Request.Headers["Authorization"]);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            await Clients.All/*User(request.ToUsername)*/.SendAsync(EFX.SignalREvents.ReceiveMessage, $"{Context.User.FindFirst(ClaimTypes.NameIdentifier).Value}: {request.Message}");// request.Message);
        }
    }
}
