using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.UseCases.PackageReviews.Create;
using EcoFarm.UseCases.PackageReviews.Delete;
using EcoFarm.UseCases.PackageReviews.Edit;
using EcoFarm.UseCases.PackageReviews.Get;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PackageReviewController : BaseController
    {
        public PackageReviewController(IMediator mediator, ILogger<PackageReviewController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Người dùng tạo đánh giá cho gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreatePackageReviewCommand command)
        {
            var result = await _mediator.Send(command);
            //if (result.IsSuccess)
            //{
            //    _hubContext.Clients.User(result.Value);
            //}
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Lấy danh sách đánh giá gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromQuery] GetListReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }
        /// <summary>
        /// Người dùng cập nhật đánh giá cho gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] EditReviewCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Người dùng xóa đánh giá
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeletePackageReviewCommand(id));
            return this.FromResult(result, _logger);
        }
    }
}
