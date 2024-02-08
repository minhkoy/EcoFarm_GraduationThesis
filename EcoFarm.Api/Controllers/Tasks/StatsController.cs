using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.UseCases.Common.Hubs;
using EcoFarm.UseCases.Statstics.GetEnterpriseStats;
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
    public class StatsController : BaseController
    {
        public StatsController(IMediator mediator, ILogger<StatsController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Lấy thống kê cho doanh nghiệp (tài khoản đang đăng nhập) theo khoảng thời gian
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEnterpriseStats([FromQuery] GetEnterpriseStatQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }
    }
}
