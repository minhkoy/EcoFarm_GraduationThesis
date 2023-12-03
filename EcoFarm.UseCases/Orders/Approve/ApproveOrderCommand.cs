using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Orders.Approve
{
    public class ApproveOrderCommand : ICommand<bool>
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
    }
    internal class ApproveOrderCommandHandler : ICommandHandler<ApproveOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public ApproveOrderCommandHandler(IUnitOfWork unitOfWork, IAuthService authService )
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result<bool>> Handle(ApproveOrderCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (string.Compare(accountType, EFX.AccountTypes.Seller) != 0)
            {
                return Result.Forbidden();
            }
            var erpId = _authService.GetAccountEntityId();
            Order order = null;
            if (!string.IsNullOrEmpty(request.OrderId))
            {
                order = await _unitOfWork.Orders.FindAsync(erpId);              
            }
            else
            {
                order = await _unitOfWork.Orders
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => string.Equals(x.CODE, request.OrderCode));
            }
            if (order is null)
            {
                return Result.NotFound("Không tìm thấy đơn hàng");
            }
            if (!string.Equals(order.ENTERPRISE_ID, erpId))
            {
                return Result.Forbidden();
            }
            if (order.STATUS != OrderStatus.WaitingSellerConfirm)
            {
                return Result.Error("Đơn hàng không ở trạng thái chờ nhà cung cấp xác nhận");
            }
            order.STATUS = OrderStatus.SellerConfirmed;
            OrderTimeline orderTimeline = new OrderTimeline()
            {
                ORDER_ID = order.ID,
                STATUS = OrderStatus.SellerConfirmed,
                TIME = DateTime.Now.ToVnDateTime()
            };
            _unitOfWork.Orders.Update(order);
            _unitOfWork.OrderTimelines.Add(orderTimeline);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Xác nhận đơn hàng thành công");
        }
    }
}
