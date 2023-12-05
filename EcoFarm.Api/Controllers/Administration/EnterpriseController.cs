using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Administration
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EnterpriseController : BaseController
    {
        public EnterpriseController(IMediator mediator, ILogger logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList([FromQuery] GetListEnterpriseQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return this.FromResult(result, _logger);
        //}
    }
}
