using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Hubs;
using EcoFarm.UseCases.Products.Create;
using EcoFarm.UseCases.Products.Delete;
using EcoFarm.UseCases.Products.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator, ILogger<ProductController> logger,
            IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Tạo sản phẩm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Lấy danh sách các sản phẩm
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList([FromQuery] GetListProductQuery command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetListByCategory([FromQuery] GetListProductByCategoryQuery command)
        //{
        //    var result = await _mediator.Send(command);
        //    return this.FromResult(result, _logger);
        //}
        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return this.FromResult(result, _logger);
        }
    }
}
