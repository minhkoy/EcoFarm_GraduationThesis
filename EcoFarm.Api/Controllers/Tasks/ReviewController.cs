using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.UseCases.Reviews.Create;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        public ReviewController(IMediator mediator, ILogger<ReviewController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Tạo đánh giá cho gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePackageReview([FromBody] CreatePackageReviewCommand command)
        {
            var result = await _mediator.Send(command);
            //if (result.IsSuccess)
            //{
            //    _hubContext.Clients.User(result.Value);
            //}
            return this.FromResult(result, _logger);
        }
    }
}
