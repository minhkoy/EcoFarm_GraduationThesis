using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.ShoppingCarts.RemoveProduct
{
    public class RemoveProductFromCartCommand : ICommand<bool>
    {
        public string CartId { get; set; }
        public List<string> ProductIds { get; set; }
    }

    internal class Handler : ICommandHandler<RemoveProductFromCartCommand, bool>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RemoveProductFromCartCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (!string.Equals(accountType, EFX.AccountTypes.Customer))
            {
                return Result.Forbidden();
            }
            var cart = await _unitOfWork.ShoppingCarts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USER_ID, _authService.GetAccountEntityId()));
            if (cart is null)
            {
                return Result.Error("Không tìm thấy thông tin giỏ hàng của bạn");
            }
            //if (cart.IS_ORDERED)
            //{
            //    return Result.Error("Giỏ hàng này đã được thanh toán");
            //}

            var cartDetail = await _unitOfWork.CartDetails
                .GetQueryable()
                .Where(x => string.Equals(x.CART_ID, cart.ID) && request.ProductIds.Contains(x.PRODUCT_ID))
                .ToListAsync();
            if (cartDetail is null)
            {
                return Result.Error("Không có sản phẩm này trong giỏ!");
            }

            _unitOfWork.CartDetails.ForceRemoveRange(cartDetail);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Xóa sản phẩm khỏi giỏ hàng thành công.");
        }
    }
}
