using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.UseCases.Common.Hubs;
using EcoFarm.UseCases.FarmingActivities.Create;
using EcoFarm.UseCases.FarmingActivities.Get;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ActivityController : BaseController
    {
        public ActivityController(IMediator mediator, ILogger<ActivityController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Tạo hoạt động mới
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateActivityCommand command)
        {
            var result = await _mediator.Send(command);
            //if (result.IsSuccess)
            //{
            //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Activity", "Create", result);
            //}
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Lấy danh sách hoạt động của một gói farming
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromQuery] GetListFarmingActivityQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }

    }
}
