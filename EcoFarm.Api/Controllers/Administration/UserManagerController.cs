using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.Application.Features.Administration.AccountManagerFeatures.Queries;
using EcoFarm.UseCases.Accounts.Lock;
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
    public class UserManagerController : BaseController
    {
        public UserManagerController(IMediator mediator, ILogger<UserManagerController> logger,
            IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }


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

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Lock([FromBody] LockAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }
    }
}
