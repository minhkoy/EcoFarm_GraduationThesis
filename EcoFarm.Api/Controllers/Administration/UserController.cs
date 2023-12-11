using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.UseCases.Users.Get;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, ILogger<UserController> logger,
            IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Lấy thông tin người dùng đang đăng nhập (user) 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMyUserInfo()
        {
            var result = await _mediator.Send(new GetMyUserInfoQuery());
            return this.FromResult(result, _logger);
        }
        //[HttpGet("[action]")]
        //public async Task<IActionResult> Get([FromQuery] GetUserQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return this.FromResult(result, _logger);
        //}
        //[HttpGet("[action]")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetListRole()
        //{
        //    var result = await _mediator.Send(new GetListRoleQuery());
        //    return this.FromResult(result, _logger);
        //}

        /// <summary>
        /// Danh sách người dùng
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetListUser([FromQuery] GetListUserQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }

    }
}
