using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.UseCases.Accounts.Get;
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
    }
}
