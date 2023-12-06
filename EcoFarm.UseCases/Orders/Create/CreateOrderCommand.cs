using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Orders.Create
{
    public class CreateOrderCommand : ICommand<OrderDTO>
    {
        /// <summary>
        /// Nếu dùng trường này: Order cho 1 sản phẩm, không dùng đến CartId
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// Số lượng. Chỉ dùng trường này khi order cho 1 sản phẩm
        /// </summary>
        public int? Quantity { get; set; }
        /// <summary>
        /// Nếu dùng trường này: Order cho nhiều sản phẩm, không dùng đến ProductId
        /// </summary>
        //public string CartId { get; set; }
        public string Note { get; set; }
        public string AddressId { get; set; }
        public OrderPaymentMethod? PaymentMethod { get; set; }

    }

    internal class CreateOrderHandler : ICommandHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public CreateOrderHandler(IUnitOfWork unitOfWork, IAuthService authService,
            ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }


        public async Task<Result<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = _authService.GetAccountEntityId();
            var accountType = _authService.GetAccountTypeName();
            if (string.Compare(accountType, EFX.AccountTypes.Customer) != 0)
            {
                return Result.Forbidden();
            }
            var user = await _unitOfWork.Users.FindAsync(userId);
            if (user is null)
            {
                return Result.Forbidden();
            }
            if (user.IS_ACTIVE)
            {
                return Result.Error(_localizeService.GetMessage(Application.Localization.LocalizationEnum.AccountLocked));
            }
            //XXX: Bỏ qua logic validator
            var address = await _unitOfWork.UserAddresses.FindAsync(request.AddressId);
            if (address is null)
            {
                return Result.Error("Không có thông tin về địa chỉ");
            }

            string code = HelperExtensions.GetRandomString(24);
            Order newOrder = new()
            {
                CODE = code,
                NAME = "Đơn hàng " + code,
                USER_ID = userId,
                ADDRESS_ID = request.AddressId,
                ADDRESS_DESCRIPTION = address.DESCRIPTION,
                PAYMENT_METHOD = request.PaymentMethod ?? OrderPaymentMethod.Cash,
                NOTE = request.Note,
                STATUS = OrderStatus.WaitingSellerConfirm,
                
            };
            
            if (!string.IsNullOrEmpty(request.ProductId))
            {
                var product = await _unitOfWork.Products.FindAsync(request.ProductId);
                if (product is null)
                {
                    return Result.Error("Không có thông tin về sản phẩm");
                }
                if (product.CURRENT_QUANTITY - request.Quantity < 0)
                {
                    return Result.Error($"Hiện nhà cung cấp chỉ còn {product.CURRENT_QUANTITY} sản phẩm. ");
                }
                var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(product.ENTERPRISE_ID);
                if (enterprise is null)
                {
                    return Result.Error("Không có thông tin về nhà cung cấp/ chủ trang trại");
                }
                OrderProduct orderProduct = new()
                {
                    ORDER_ID = newOrder.ID,
                    PRODUCT_ID = request.ProductId,
                    QUANTITY = request.Quantity ?? 1,
                    PRICE = product.PRICE ?? 0,
                    CURRENCY = product.CURRENCY,                    
                };
                newOrder.TOTAL_PRICE = orderProduct.PRICE.Value * orderProduct.QUANTITY;
                newOrder.TOTAL_QUANTITY = orderProduct.QUANTITY;
                product.SOLD += orderProduct.QUANTITY;

                OrderTimeline newTimeline = new()
                {
                    ORDER_ID = newOrder.ID,
                    STATUS = OrderStatus.WaitingSellerConfirm,
                    TIME = newOrder.CREATED_TIME
                };

                _unitOfWork.Products.Update(product);
                _unitOfWork.OrderProducts.Add(orderProduct);
                _unitOfWork.OrderTimelines.Add(newTimeline);
                _unitOfWork.Orders.Add(newOrder); 
                await _unitOfWork.SaveChangesAsync();
                return Result.Success(new OrderDTO
                {
                    OrderId = newOrder.ID,
                    OrderCode = newOrder.CODE,
                    Name = newOrder.NAME,
                    UserId = newOrder.USER_ID,
                    AddressId = newOrder.ADDRESS_ID,
                    AddressDescription = newOrder.ADDRESS_DESCRIPTION,
                    Note = newOrder.NOTE,
                    TotalQuantity = newOrder.TOTAL_QUANTITY,
                    TotalPrice = newOrder.TOTAL_PRICE,
                    PaymentMethod = newOrder.PAYMENT_METHOD,
                    Status = newOrder.STATUS,
                    SellerEnterpriseId = newOrder.ENTERPRISE_ID,
                    SellerEnterpriseName = enterprise.NAME,
                    CreatedAt = newOrder.CREATED_TIME,
                    UpdatedAt = newOrder.MODIFIED_TIME,
                }, "Thêm mới đặt hàng thành công");
            }
            //else if (!string.IsNullOrEmpty(request.CartId))
            //{
            //    var cart = await _unitOfWork.ShoppingCarts.FindAsync(request.CartId);
            //    if (cart is null)
            //    {
            //        return Result.Error("Không có thông tin về giỏ hàng");
            //    }
            //    var cartDetails = await _unitOfWork.CartDetails
            //        .GetQueryable()
            //        .Where(x => x.CART_ID == request.CartId)
            //        .ToListAsync();
            //    if (cartDetails is null || cartDetails.Count == 0)
            //    {
            //        return Result.Error("Giỏ hàng rỗng. Không thể tạo đơn hàng");
            //    }
            //    List<OrderProduct> orderProducts = new();
            //    cartDetails.ForEach(x =>
            //    {
            //        orderProducts.Add(new()
            //        {
            //            ORDER_ID = newOrder.ID,
            //            PRODUCT_ID = x.PRODUCT_ID,
            //            QUANTITY = x.QUANTITY ?? 1,
            //            PRICE = x.PRICE,
            //            CURRENCY = x.CURRENCY,                        
            //        });
            //    });
            //    cart.IS_ORDERED = true;
            //    cart.TOTAL_PRICE = orderProducts.Sum(x => x.PRICE.Value * x.QUANTITY);
            //    cart.TOTAL_QUANTITY = orderProducts.Sum(x => x.QUANTITY);
            //    newOrder.TOTAL_PRICE = cart.TOTAL_PRICE.Value;
            //    newOrder.TOTAL_QUANTITY = cart.TOTAL_QUANTITY.Value;
            //    _unitOfWork.ShoppingCarts.Update(cart);
            //    _unitOfWork.OrderProducts.AddRange(orderProducts);
            //    _unitOfWork.Orders.Add(newOrder);
            //}
            else
            {
                return Result.Error("Không thể đặt hàng. Vui lòng kiểm tra lại thông tin");
            }
            //await _unitOfWork.SaveChangesAsync();
            
        }
    }
}
