using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.UseCases.Common.Hubs;
using EcoFarm.UseCases.Orders.Approve;
using EcoFarm.UseCases.Orders.Cancel;
using EcoFarm.UseCases.Orders.Create;
using EcoFarm.UseCases.Orders.Get;
using EcoFarm.UseCases.Orders.MarkPreparing;
using EcoFarm.UseCases.Orders.MarkPreparingCompleted;
using EcoFarm.UseCases.Orders.MarkReceived;
using EcoFarm.UseCases.Orders.Reject;
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
        /// Danh sách đơn hàng mà NCC/ chủ trang trại quản lý hoặc của người dùng thực hiện
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListOrderQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }
        /// <summary>
        /// Nhà cung cấp/ chủ nông trại xác nhận đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Approve([FromRoute] string id)
        {
            var result = await _mediator.Send(new ApproveOrderCommand(id));
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Người dùng hủy đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Cancel([FromRoute] string id)
        {
            var result = await _mediator.Send(new CancelOrderCommand(id));
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
        public async Task<IActionResult> Get([FromQuery] GetOrderQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đánh dấu đơn hàng đang được chuẩn bị
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> MarkPreparing([FromRoute] string id)
        {
            var result = await _mediator.Send(new MarkPreparingCommand(id));
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Người bán từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> RejectOrder([FromRoute] string id)
        {
            var result = await _mediator.Send(new RejectOrderCommand(id));
            return this.FromResult(result, _logger);
        }
        /// <summary>
        /// Đánh dấu đơn hàng đã chuẩn bị xong và bắt đầu bàn giao vận chuyển
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// Description:
        /// 
        /// - Do chưa áp dụng vận chuyển, tạm thời coi như trạng thái đơn là Đã giao hàng
        /// </remarks>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> MarkPreparingCompleted([FromRoute] string id)
        {
            var result = await _mediator.Send(new MarkPreparingCompletedCommand(id));
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đánh dấu đơn hàng đã nhận
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> MarkReceived([FromRoute] string id)
        {
            var result = await _mediator.Send(new MarkReceivedOrderCommand(id));
            return this.FromResult(result, _logger);
        }
    }
}
