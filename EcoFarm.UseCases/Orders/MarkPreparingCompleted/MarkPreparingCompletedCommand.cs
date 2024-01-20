using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Orders.MarkPreparingCompleted
{
    public class MarkPreparingCompletedCommand : ICommand<bool>
    {
        public string OrderId { get; set; }
        public MarkPreparingCompletedCommand() { }
        public MarkPreparingCompletedCommand(string orderId)
        {
            OrderId = orderId;
        }
    }

    internal class MarkPreparingCompletedHandler : ICommandHandler<MarkPreparingCompletedCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public MarkPreparingCompletedHandler(IUnitOfWork unitOfWork, IAuthService authService,
                       ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<bool>> Handle(MarkPreparingCompletedCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(_authService.GetAccountTypeName(), EFX.AccountTypes.Seller))
            {
                return Result.Forbidden();
            }
            var erpId = _authService.GetAccountEntityId();
            var order = await _unitOfWork.Orders.FindAsync(request.OrderId);
            if (order is null)
            {
                return Result.NotFound(_localizeService.GetMessage(LocalizationEnum.OrderNotFound));
            }
            if (!string.Equals(order.ENTERPRISE_ID, erpId))
            {
                return Result.Forbidden();
            }
            if (order.STATUS != OrderStatus.Preparing)
            {
                return Result.Error("Đơn hàng hiện đang không ở trạng thái {1}", EFX.OrderStatuses.Preparing);
            }
            order.STATUS = OrderStatus.Shipped; //XX: Must be fixed to -> OrderStatus.PreparingCompleted when shipping feature is ready.
            _unitOfWork.Orders.Update(order);
            return Result.SuccessWithMessage("Đánh dấu đơn hàng đã hoàn tất chuẩn bị thành công");
        }
    }
}
