using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.Common.Hubs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Common.Hubs
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
            Console.WriteLine("Hi");
            var name = _authService.GetFullname();
            Task.FromResult(Clients.All.SendAsync(EFX.SignalREvents.ReceiveMessage, $"Xin chào {name} !!!"));
            var userId = _authService.GetAccountEntityId();
            Console.WriteLine("Account entity id: " + userId);
            var identity = new ClaimsIdentity(userId);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
            //identity.AddClaim(new Claim(ClaimTypes.Name, _authService.GetFullname()));
            //var principal = new ClaimsPrincipal(identity);
            Context.User.AddIdentity(identity);
            Console.WriteLine("Token: " + httpContextAccessor.HttpContext.Request.Query["access_token"]);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(SendMessageRequest request)
        {
            Console.WriteLine("??????");
            await Clients.All/*User(request.ToUsername)*/.SendAsync(EFX.SignalREvents.ReceiveMessage, $"{Context.User.FindFirst(ClaimTypes.NameIdentifier).Value}: {request.Message}");// request.Message);
        }

        public async Task Send(string message)
        {
            Console.WriteLine("ABC");
            await Clients.All/*User(request.ToUsername)*/.SendAsync(EFX.SignalREvents.ReceiveMessage, $"{Context.User.FindFirst(ClaimTypes.NameIdentifier).Value}: {message}");// request.Message);

        }

    }
}
