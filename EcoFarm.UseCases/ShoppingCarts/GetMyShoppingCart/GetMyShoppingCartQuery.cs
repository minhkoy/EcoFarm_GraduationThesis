using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.ShoppingCarts.GetMyShoppingCart
{
    public class GetMyShoppingCartQuery : IQuerySingle<ShoppingCartDTO>
    {

    }

    internal class Handler : IQuerySingleHandler<GetMyShoppingCartQuery, ShoppingCartDTO>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<ShoppingCartDTO>> Handle(GetMyShoppingCartQuery request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (!string.Equals(accountType, EFX.AccountTypes.Customer))
            {
                return Result.Forbidden();
            }
            var userId = _authService.GetAccountEntityId();
            var cart = await _unitOfWork.ShoppingCarts
                .GetQueryable()
                .Include(x => x.CartDetails)
                .ThenInclude(x => x.ProductInfo)
                .ThenInclude(x => x.ProductMedias)
                .Where(x => string.Equals(x.USER_ID, userId))
                .FirstOrDefaultAsync();
            if (cart is null)
            {
                return Result.Success(new ShoppingCartDTO());
            }
            var cartDTO = new ShoppingCartDTO
            {
                Id = cart.ID,
                IsOrdered = cart.IS_ORDERED,
                TotalPrice = cart.TOTAL_PRICE,
                TotalQuantity = cart.TOTAL_QUANTITY,
                Products = cart.CartDetails.Select(x => new CartDetailDTO
                {
                    ProductId = x.PRODUCT_ID,
                    ProductName = x.ProductInfo.NAME,
                    ProductImage = x.ProductInfo.ProductMedias.Any() ? x.ProductInfo.ProductMedias.FirstOrDefault().MEDIA_URL : string.Empty,
                    ProductPrice = x.ProductInfo.PRICE,
                    
                    Quantity = x.ProductInfo.CURRENT_QUANTITY
                }).ToList()
            };
            return Result.Success(cartDTO);
        }
    }
}
