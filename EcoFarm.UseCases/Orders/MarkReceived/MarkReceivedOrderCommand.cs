using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Orders.MarkReceived
{
    public class MarkReceivedOrderCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public MarkReceivedOrderCommand(string id)
        {
            Id = id;
        }
        public MarkReceivedOrderCommand()
        {

        }
    }

    internal class MarkReceivedOrderHandler : ICommandHandler<MarkReceivedOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public MarkReceivedOrderHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(MarkReceivedOrderCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (accountType != EFX.AccountTypes.Customer)
            {
                return Result.Forbidden();
            }
            var order = await _unitOfWork.Orders.FindAsync(request.Id);
            if (order is null)
            {
                return Result.NotFound("Không tìm thấy thông tin đơn hàng");
            }
            if (!string.Equals(order.USER_ID, _authService.GetAccountEntityId()))
            {
                return Result.Forbidden();
            }
            if (order.STATUS != OrderStatus.Shipped)
            {
                return Result.Error("Đơn hàng chưa ở trạng thái đã vận chuyển đến");
            }
            order.STATUS = OrderStatus.Received;
            OrderTimeline orderTimeline = new()
            {
                ORDER_ID = order.ID,
                STATUS = order.STATUS,
                TIME = DateTime.Now.ToVnDateTime(),
            };
            _unitOfWork.OrderTimelines.Add(orderTimeline);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Cập nhật trạng thái đơn hàng thành công");
        }
    }

}
