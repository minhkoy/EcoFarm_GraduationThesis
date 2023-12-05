using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.Application.Common.Results;
using EcoFarm.UseCases.Accounts.Login;
using EcoFarm.UseCases.Accounts.Logout;
using EcoFarm.UseCases.Accounts.Signup;
using EcoFarm.UseCases.DTOs;



//using EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TokenHandler.Interfaces;

namespace EcoFarm.Api.Controllers.Administration
{
    /// <summary>
    /// Nhóm apis đăng nhập, đăng ký, đăng xuất
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //XXX: It must be fixed, Roles must be called with enum or another way, not fixed string.
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IMediator mediator, IAuthService authService
            , ILogger<AuthenticationController> logger,
            IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng nhập vào hệ thống
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Result<LoginDTO>), 200)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đăng ký với vai trò là người dùng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Result<UserDTO>), 200)]
        public async Task<IActionResult> SignupAsUser([FromBody] SignupAsUserCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đăng ký với vai trò là doanh nghiệp
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Result<EnterpriseDTO>), 200)]
        public async Task<IActionResult> SignupAsEnterprise([FromBody] SignupAsEnterpriseCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        //[HttpPost("[action]")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Signup([FromBody] SignupAsSellerCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return this.FromResult(result, _logger);
        //}

        /// <summary>
        /// Lấy thông tin user hiện tại --- XXX: Đang implement không đúng, phục vụ cho việc test
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserInfo()
        {
            //return Ok(_authService.GetUsername());
            Account account = new Account(
  "djknwfl1f",
  "385441744582981",
  "dvF_HjGR1qPgoA1uCz3HbJ206SI");

            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"https://upload.wikimedia.org/wikipedia/commons/a/ae/Olympic_flag.jpg"),
                PublicId = "olympic_flag"
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            if (uploadResult != null) return Ok(uploadResult.Url);
            return BadRequest();
        }

        /// <summary>
        /// Lưu thông tin đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpPatch("[action]")]
        [ProducesResponseType(typeof(Result<bool>), 200)]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new LogoutAccountCommand());
            return this.FromResult(result, _logger);
        }
    }
}
