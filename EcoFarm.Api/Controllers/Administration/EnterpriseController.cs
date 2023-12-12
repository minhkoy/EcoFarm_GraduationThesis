using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.UseCases.Enterprises.Get;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Administration
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EnterpriseController : BaseController
    {
        public EnterpriseController(IMediator mediator, ILogger logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Lấy thông tin doanh nghiệp của tài khoản đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMyInfo()
        {
            var result = await _mediator.Send(new GetMyEnterpriseInfoQuery());
            return this.FromResult(result, _logger);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetList([FromQuery] GetListEnterpriseQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return this.FromResult(result, _logger);
        //}
    }
}
