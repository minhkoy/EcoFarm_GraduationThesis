﻿using EcoFarm.Api.Abstraction.Extensions;
using EcoFarm.Api.Abstraction.Hubs;
using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Approve;
using EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Reject;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.FarmingActivities.Create;
using EcoFarm.UseCases.FarmingPackages.Approve;
using EcoFarm.UseCases.FarmingPackages.CloseRegister;
using EcoFarm.UseCases.FarmingPackages.Create;
using EcoFarm.UseCases.FarmingPackages.Get;
using EcoFarm.UseCases.FarmingPackages.Register;
using EcoFarm.UseCases.FarmingPackages.Start;
using EcoFarm.UseCases.FarmingPackages.Update;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Api.Controllers.Administration
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FarmingPackageController : BaseController
    {
        public FarmingPackageController(IMediator mediator, ILogger<FarmingPackageController> logger,
            IHubContext<NotificationHub> hubContext) : base(mediator, logger, hubContext)
        {
        }

        /// <summary>
        /// Lấy danh sách gói farming
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListPackageQuery query)
        {
            var result = await _mediator.Send(query);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Tạo gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFarmingPackageCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Cập nhật thông tin gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFarmingPackageCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Thêm mới hoạt động của gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đăng ký gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPackageCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Đóng đăng ký gói farming
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> CloseRegister([FromRoute] string id)
        {
            var result = await _mediator.Send(new CloseRegisterPackageCommand(id));
            return this.FromResult(result, _logger);
        }
        /// <summary>
        /// Bắt đầu gói farming
        /// </summary>
        /// <param name="id">Id gói farming</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Start([FromRoute] string id)
        {
            var result = await _mediator.Send(new StartFarmingPackageCommand(id));
            if (result.IsSuccess)
            {
                var data = result.Value;

                var message = $"Gói farming {data.Name} đã được bắt đầu lúc {data.StartTime}";
                data.RegisteredUsers.ForEach(user =>
                {
                    _hubContext.Clients.User(user.AccountId).SendAsync(nameof(EventType.NotifyFarmingEvent), message);
                });
            }
            return this.FromResult(result, _logger);
        }

        /// <summary>
        /// Phê duyệt gói farming
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Approve([FromRoute] string id)
        {
            return await base.ResultFromMediator<ApprovePackageCommand, bool>(new ApprovePackageCommand(id));
            //var result = await _mediator.Send(new ApprovePackageCommand(id));
            //return this.FromResult(result, _logger);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{nameof(RoleType.Admin)}")]
        /// <summary>
        /// Từ chối gói farming
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Reject([FromBody] RejectServiceCommand command)
        {
            var result = await _mediator.Send(command);
            return this.FromResult(result, _logger);
        }
    }
    
}
