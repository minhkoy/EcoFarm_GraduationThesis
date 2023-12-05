using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.UseCases.Accounts.ChangePassword;
using EcoFarm.UseCases.Accounts.Get;
using EcoFarm.UseCases.Accounts.Lock;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator, ILogger<AccountController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {

        }

        /// <summary>
        /// Lấy danh sách các loại tài khoản
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListAccountType()
        {
            var result = await _mediator.Send(new GetListAccountTypeQuery());
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> LockAccount([FromBody] LockAccountCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Thay đổi mật khẩu
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }
    }
}
