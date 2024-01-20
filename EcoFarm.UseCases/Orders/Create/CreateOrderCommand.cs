using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs;
using EcoFarm.UseCases.DTOs;
using Hangfire;
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
        /// Nếu dùng trường này: Order cho nhiều sản phẩm (trong giỏ hàng), không dùng đến ProductId
        /// </summary>
        //public string CartId { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public string Note { get; set; }
        public string AddressId { get; set; }
        public OrderPaymentMethod? PaymentMethod { get; set; }
        public class CartProduct
        {
            public string ProductId { get; set; }
            public int? Quantity { get; set; }
        }
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
            if (!user.IS_ACTIVE)
            {
                var account = await _unitOfWork.Accounts.FindAsync(user.ACCOUNT_ID);
                return Result.Error(string.Format(_localizeService.GetMessage(Application.Localization.LocalizationEnum.AccountLocked), account?.LOCKED_REASON ?? "không xác định"));
            }
            var address = await _unitOfWork.UserAddresses.FindAsync(request.AddressId);
            if (address is null)
            {
                return Result.Error("Không có thông tin về địa chỉ");
            }            
            
            if (!string.IsNullOrEmpty(request.ProductId))
            {
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
                    RECEIVER_NAME = address.RECEIVER_NAME,
                    RECEIVER_PHONE = address.PHONE_NUMBER,
                };
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

                decimal price = 0;
                if (!string.IsNullOrEmpty(product.PACKAGE_ID))
                {
                    var package = await _unitOfWork.FarmingPackages.FindAsync(product.PACKAGE_ID);
                    if (package is null)
                    {
                        return Result.Error("Không có thông tin về gói farming");
                    }
                    if (!package.IS_ACTIVE)
                    {
                        return Result.Error("Gói farming đã bị khóa");
                    }
                    if (!package.END_TIME.HasValue)
                    {
                        return Result.Error("Gói farming chưa kết thúc. Không thể mua sản phẩm này ngay");
                    }
                    var isUserRegisterPackage = await _unitOfWork.UserRegisterPackages
                        .GetQueryable()
                        .AnyAsync(x => string.Equals(x.PACKAGE_ID, package.ID) && string.Equals(x.USER_ID, userId));
                    if (!isUserRegisterPackage)
                    {
                        price = product.PRICE ?? 0;
                    }
                    else
                    {
                        price = product.PRICE_FOR_REGISTERED ?? product.PRICE ?? 0;
                    }

                }
                OrderProduct orderProduct = new()
                {
                    ORDER_ID = newOrder.ID,
                    PRODUCT_ID = request.ProductId,
                    QUANTITY = request.Quantity ?? 1,
                    PRICE = price,
                    CURRENCY = product.CURRENCY,                    
                };
                newOrder.TOTAL_PRICE = orderProduct.PRICE.Value * orderProduct.QUANTITY;
                newOrder.TOTAL_QUANTITY = orderProduct.QUANTITY;
                newOrder.ENTERPRISE_ID = enterprise.ID;
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
                    ReceiverName = newOrder.RECEIVER_NAME,
                    ReceiverPhone = newOrder.RECEIVER_PHONE,
                }, "Thêm mới đặt hàng thành công");
            }
            else if (request.CartProducts is not null && request.CartProducts.Count > 0)
            {
                var orders = new List<Order>();
                var orderProducts = new List<OrderProduct>();
                foreach (var cartProduct in request.CartProducts)
                {
                    if (string.IsNullOrEmpty(cartProduct.ProductId) || !cartProduct.Quantity.HasValue || cartProduct.Quantity.Value == 0) continue;
                    var product = await _unitOfWork.Products.FindAsync(cartProduct.ProductId);
                    if (product is null)
                    {
                        return Result.Error($"Sản phẩm {cartProduct.ProductId} không tồn tại hoặc đã bị xóa. Vui lòng kiểm tra lại");
                    }
                    if (product.CURRENT_QUANTITY - cartProduct.Quantity < 0)
                    {
                        return Result.Error($"Hiện nhà cung cấp chỉ còn {product.CURRENT_QUANTITY} sản phẩm. ");
                    }
                    var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(product.ENTERPRISE_ID);
                    if (enterprise is null)
                    {
                        return Result.Error("Không có thông tin về nhà cung cấp/ chủ trang trại");
                    }

                    decimal price = 0;
                    if (!string.IsNullOrEmpty(product.PACKAGE_ID))
                    {
                        var package = await _unitOfWork.FarmingPackages.FindAsync(product.PACKAGE_ID);
                        if (package is null)
                        {
                            return Result.Error($"Không có thông tin về gói farming của sản phẩm {product.NAME}");
                        }
                        if (!package.IS_ACTIVE)
                        {
                            return Result.Error("Gói farming đã bị khóa");
                        }
                        if (!package.END_TIME.HasValue)
                        {
                            return Result.Error("Gói farming chưa kết thúc. Không thể mua sản phẩm này ngay");
                        }
                        var isUserRegisterPackage = await _unitOfWork.UserRegisterPackages
                            .GetQueryable()
                            .AnyAsync(x => string.Equals(x.PACKAGE_ID, package.ID) && string.Equals(x.USER_ID, userId));
                        if (!isUserRegisterPackage)
                        {
                            price = product.PRICE ?? 0;
                        }
                        else
                        {
                            price = product.PRICE_FOR_REGISTERED ?? product.PRICE ?? 0;
                        }

                    }
                    else
                    {
                        price = product.PRICE ?? 0;
                    }

                    var existedOrder = orders.FirstOrDefault(x => string.Equals(x.ENTERPRISE_ID, product.ENTERPRISE_ID));
                    if (existedOrder is not null)
                    {
                        existedOrder.TOTAL_PRICE += price * cartProduct.Quantity.Value;
                        existedOrder.TOTAL_QUANTITY += cartProduct.Quantity.Value;
                        orderProducts.Add(new OrderProduct
                        {
                            ORDER_ID = existedOrder.ID,
                            PRODUCT_ID = cartProduct.ProductId,
                            QUANTITY = cartProduct.Quantity ?? 1,
                            PRICE = price,
                            CURRENCY = product.CURRENCY,
                        });
                        product.SOLD += cartProduct.Quantity ?? 1;
                        _unitOfWork.Products.Update(product);
                    }
                    else
                    {
                        var code = HelperExtensions.GetRandomString(24);
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
                            RECEIVER_NAME = address.RECEIVER_NAME,
                            RECEIVER_PHONE = address.PHONE_NUMBER,
                            TOTAL_PRICE = price * cartProduct.Quantity.Value,
                            TOTAL_QUANTITY = cartProduct.Quantity.Value,
                            ENTERPRISE_ID = enterprise.ID,
                        };
                        //orders.Add(newOrder);
                        OrderProduct orderProduct = new()
                        {
                            ORDER_ID = newOrder.ID,
                            PRODUCT_ID = cartProduct.ProductId,
                            QUANTITY = cartProduct.Quantity ?? 1,
                            PRICE = price,
                            CURRENCY = product.CURRENCY,
                        };
                        newOrder.TOTAL_PRICE = orderProduct.PRICE.Value * orderProduct.QUANTITY;
                        newOrder.TOTAL_QUANTITY = orderProduct.QUANTITY;
                        newOrder.ENTERPRISE_ID = enterprise.ID;
                        product.SOLD += orderProduct.QUANTITY;

                        OrderTimeline newTimeline = new()
                        {
                            ORDER_ID = newOrder.ID,
                            STATUS = OrderStatus.WaitingSellerConfirm,
                            TIME = newOrder.CREATED_TIME
                        };
                        orderProducts.Add(orderProduct);
                        orders.Add(newOrder);
                        _unitOfWork.Products.Update(product);
                    }                    
                }

                _unitOfWork.OrderProducts.AddRange(orderProducts);
                //_unitOfWork.OrderTimelines.Add(newTimeline);
                _unitOfWork.Orders.AddRange(orders);
                await _unitOfWork.SaveChangesAsync();
                //BackgroundJob.Enqueue<RemoveProductsFromCartJob>(x => x.Run(userId, orderProducts.Select(x => x.PRODUCT_ID).ToList()));
                return Result.Success(new OrderDTO
                {
                    //OrderId = newOrder.ID,
                    //OrderCode = newOrder.CODE,
                    //Name = newOrder.NAME,
                    //UserId = newOrder.USER_ID,
                    //AddressId = newOrder.ADDRESS_ID,
                    //AddressDescription = newOrder.ADDRESS_DESCRIPTION,
                    //Note = newOrder.NOTE,
                    //TotalQuantity = newOrder.TOTAL_QUANTITY,
                    //TotalPrice = newOrder.TOTAL_PRICE,
                    //PaymentMethod = newOrder.PAYMENT_METHOD,
                    //Status = newOrder.STATUS,
                    //SellerEnterpriseId = newOrder.ENTERPRISE_ID,
                    //SellerEnterpriseName = enterprise.NAME,
                    //CreatedAt = newOrder.CREATED_TIME,
                    //UpdatedAt = newOrder.MODIFIED_TIME,
                    //ReceiverName = newOrder.RECEIVER_NAME,
                    //ReceiverPhone = newOrder.RECEIVER_PHONE,
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
