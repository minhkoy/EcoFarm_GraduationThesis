using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.UseCases.Orders.Approve;
using EcoFarm.UseCases.Orders.Cancel;
using EcoFarm.UseCases.Orders.Create;
using EcoFarm.UseCases.Orders.Get;
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
    public class OrderController : BaseController
    {
        public OrderController(IMediator mediator, ILogger<OrderController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Người dùng tạo một order mới
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Nhà cung cấp/ chủ nông trại xác nhận đơn hàng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Approve([FromBody] ApproveOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Người dùng hủy đơn hàng
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Cancel([FromBody] CancelOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
        //{
        //    var result = await _mediator.Send(query);
        //    return this.FromResult(result, _logger);
        //}

        /// <summary>
        /// Lấy thông tin của một order
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOrder([FromQuery] GetOrderQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }
    }
}
