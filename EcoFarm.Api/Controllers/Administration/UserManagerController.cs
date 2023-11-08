using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Application.Features.Administration.AccountManagerFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = $"{nameof(RoleType.Admin)}")]
    public class UserManagerController : BaseController
    {
        public UserManagerController(IMediator mediator) : base(mediator)
        {
        }


        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListRole()
        {
            var result = await _mediator.Send(new GetListRoleQuery());
            return this.FromResult(result);
        }
    }
}
