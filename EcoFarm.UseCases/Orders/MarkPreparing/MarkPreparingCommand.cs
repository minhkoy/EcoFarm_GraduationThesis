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

namespace EcoFarm.UseCases.Orders.MarkPreparing
{
    public class MarkPreparingCommand : ICommand<bool>
    {
        public string OrderId { get; set; }
        public MarkPreparingCommand() { }
        public MarkPreparingCommand(string orderId)
        {
            OrderId = orderId;
        }
    }

    internal class Handler : ICommandHandler<MarkPreparingCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService,
            ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<bool>> Handle(MarkPreparingCommand request, CancellationToken cancellationToken)
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
            if (order.STATUS != OrderStatus.SellerConfirmed)
            {
                return Result.Error("Đơn hàng hiện đang không ở trạng thái {1}", EFX.OrderStatuses.SellerConfirmed);
            }
            order.STATUS = OrderStatus.Preparing;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Đơn hàng hiện ở trạng thái Đang chuẩn bị hàng");
        }
    }
}
