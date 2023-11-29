using Ardalis.Result;
using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.Application.Interfaces.Messagings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMediator _mediator { get; set; }
        protected ILogger _logger { get; set; }
        protected IHubContext<NotificationHub> _hubContext { get; set; }

        public BaseController(IMediator mediator, ILogger logger, IHubContext<NotificationHub> hubContext)
        {
            _mediator = mediator;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected async Task<IActionResult> ResultFromMediator<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<Result<TResponse>>
        {
            var result = await _mediator.Send(request);
            return this.FromResult(result, _logger);
        }
    }
}