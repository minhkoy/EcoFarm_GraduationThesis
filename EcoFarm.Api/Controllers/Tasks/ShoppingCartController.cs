using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.UseCases.Common.Hubs;
using EcoFarm.UseCases.ShoppingCarts.AddNewProduct;
using EcoFarm.UseCases.ShoppingCarts.GetMyShoppingCart;
using EcoFarm.UseCases.ShoppingCarts.RemoveProduct;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(IMediator mediator, ILogger<ShoppingCartController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddNewProduct([FromRoute] string productId)
        {
            var result = await _mediator.Send(new AddNewProductToCartCommand(productId));
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Xóa sản phẩm trong giỏ hàng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductFromCartCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Lấy thông tin giỏ hàng của người dùng. Sẽ hỗ trợ nhiều giỏ hàng cho một người sau
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var result = await _mediator.Send(new GetMyShoppingCartQuery());
            return this.FromResult(result, _logger);
        }

    }
}
