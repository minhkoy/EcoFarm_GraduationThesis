using EcoFarm.Application.Features.Tasks.ServicePackageFeatures.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcoFarm.Domain.Common.Values.Constants.EFX;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServicePackageController : BaseController
    {
        public ServicePackageController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = nameof(RoleType.Seller))]
        [HttpPost]
        public async Task<IActionResult> CreateServicePackage([FromBody] CreateServiceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
