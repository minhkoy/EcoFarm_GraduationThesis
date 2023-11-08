using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Approve;
using EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Reject;
using EcoFarm.Domain.Common.Values.Constants;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Roles = $"{nameof(RoleType.Admin)}")]
    public class ServiceManagerController : BaseController
    {
        public ServiceManagerController(IMediator mediator) : base(mediator)
        {
        }

        
        [HttpPost]
        public async Task<IActionResult> ApproveService([FromBody] ApproveServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(RoleType.Admin)}")]
        [HttpPost]
        public async Task<IActionResult> RejectService([FromBody] RejectServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result);
        }
    }
    
}
