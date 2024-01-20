using Ardalis.Result;
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

namespace EcoFarm.UseCases.ShoppingCarts.AddNewProduct
{
    public class AddNewProductToCartCommand : ICommand<bool>
    {
        public string ProductId { get; set; }
        public AddNewProductToCartCommand(string productId)
        {
            ProductId = productId;
        }
        public AddNewProductToCartCommand()
        {

        }
    }

    internal class Handler : ICommandHandler<AddNewProductToCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(AddNewProductToCartCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (!string.Equals(accountType, EFX.AccountTypes.Customer))
            {
                return Result.Forbidden();
            }
            var userId = _authService.GetAccountEntityId();

            var product = await _unitOfWork.Products.FindAsync(request.ProductId);
            if (product is null)
            {
                return Result.Error("Không có thông tin sản phẩm");
            }
            if (!product.IS_ACTIVE)
            {
                return Result.Error("Sản phẩm đang bị khóa");
            }

            var cart = await _unitOfWork.ShoppingCarts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USER_ID, userId), cancellationToken);
            if (cart is null)
            {
                cart = new ShoppingCart
                {
                    USER_ID = userId,
                    TOTAL_PRICE = 0,
                    TOTAL_QUANTITY = 1
                };
                _unitOfWork.ShoppingCarts.Add(cart);
                _unitOfWork.CartDetails.Add(new CartDetail
                {
                    CART_ID = cart.ID,
                    PRODUCT_ID = request.ProductId,
                    QUANTITY = 1,
                });
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            else
            {
                if (cart.TOTAL_QUANTITY > 20)
                {
                    return Result.Error("Chỉ cho phép 20 loại sản phẩm cho một giỏ hàng");
                }
                var cartDetail = await _unitOfWork.CartDetails
                    .GetQueryable()
                    .FirstOrDefaultAsync(x => string.Equals(x.CART_ID, cart.ID) && string.Equals(x.PRODUCT_ID, request.ProductId), cancellationToken);
                if (cartDetail is null)
                {
                    cart.TOTAL_QUANTITY += 1;
                    _unitOfWork.ShoppingCarts.Update(cart);
                    _unitOfWork.CartDetails.Add(new CartDetail
                    {
                        CART_ID = cart.ID,
                        PRODUCT_ID = request.ProductId,
                        QUANTITY = 1,
                    });
                }
                else
                {
                    return Result.Error("Sản phẩm đã có trong giỏ hàng của bạn!");
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return Result<bool>.Success(true);
        }
    }
}
