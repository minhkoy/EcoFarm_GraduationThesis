using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.UseCases.UserAddresses.Create;
using EcoFarm.UseCases.UserAddresses.Delete;
using EcoFarm.UseCases.UserAddresses.Get;
using EcoFarm.UseCases.UserAddresses.SetMain;
using EcoFarm.UseCases.UserAddresses.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EcoFarm.Api.Controllers.Tasks
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : BaseController
    {
        public AddressController(IMediator mediator, ILogger<AddressController> logger, IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Tạo mới địa chỉ
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Lấy danh sách địa chỉ
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListUserAddressQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Cập nhật địa chỉ    
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đặt địa điểm nhận mặc định
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> SetMain([FromRoute] string id)
        {
            var result = await _mediator.Send(new SetMainAddressCommand(id));
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Người dùng xóa địa chỉ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var result = await _mediator.Send(new DeleteAddressCommand(id));
            return this.FromResult(result, _logger);
        }
    }
}
