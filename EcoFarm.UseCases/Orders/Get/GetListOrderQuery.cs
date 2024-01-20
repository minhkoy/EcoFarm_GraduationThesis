using Ardalis.Result;
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

namespace EcoFarm.UseCases.Orders.Get
{
    public class GetListOrderQuery : IQuery<OrderDTO>
    {
        //public string EnterpriseId { get; set; }
        //public string UserId { get; set; }
        public string Id { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        /// <summary>
        /// Keyword cho mã đơn hàng (code) và note
        /// </summary>
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class GetListOrderHandler : IQueryHandler<GetListOrderQuery, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetListOrderHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<List<OrderDTO>>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            //if (!string.Equals(accountType, EFX.AccountTypes.Seller))
            //{
            //    return Result.Forbidden();
            //}
            IQueryable<Order> query = _unitOfWork.Orders
                .GetQueryable()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.ProductInfo);
            switch (accountType)
            {
                case EFX.AccountTypes.Customer:
                    query = query.Where(x => string.Equals(x.USER_ID, _authService.GetAccountEntityId()));
                    break;
                case EFX.AccountTypes.Seller:
                    query = query.Where(x => string.Equals(x.ENTERPRISE_ID, _authService.GetAccountEntityId()));
                    Console.WriteLine(_authService.GetAccountEntityId());
                    break;
            }
                //.Where(x => string.Equals(x.ENTERPRISE_ID, _authService.GetAccountEntityId()));              
            
            //if (!string.IsNullOrWhiteSpace(request.UserId))
            //{
            //    if (!string.Equals(accountType, EFX.AccountTypes.Customer))
            //    {
            //        return Result.Forbidden();
            //    }
            //    query = query.Where(x => string.Equals(x.USER_ID, request.UserId));
            //}
            //if (!string.IsNullOrEmpty(request.EnterpriseId))
            //{
            //    query = query.Where(x => string.Equals(x.ENTERPRISE_ID, request.EnterpriseId));
            //}
            if (!string.IsNullOrEmpty(request.Id))
            {
                var order = await query.FirstOrDefaultAsync(x => string.Equals(x.ID, request.Id));
                return Result.Success(new List<OrderDTO> { new() {
                    OrderId = order.ID,
                    OrderCode = order.CODE,
                    CreatedAt = order.CREATED_TIME,
                    AddressId = order.ADDRESS_ID,
                    AddressDescription = order.ADDRESS_DESCRIPTION,
                    TotalPrice = order.TOTAL_PRICE,
                    ReceiverName = order.RECEIVER_NAME,
                    ReceiverPhone = order.RECEIVER_PHONE,
                    Note = order.NOTE,
                    Status = order.STATUS,
                    TotalQuantity = order.TOTAL_QUANTITY,
                    Name = order.NAME,
                    UserId = order.USER_ID,
                    ListProducts = order.OrderProducts.Select(x => new ProductDTO
                    {
                        Code = x.ProductInfo.CODE,
                        Name = x.ProductInfo.NAME,
                        Id = x.PRODUCT_ID

                    }).ToList(),

                } });
            }

            if (request.Status.HasValue && request.Status.Value > 0)
            {
                query = query.Where(x => (int)x.STATUS == request.Status.Value);
            }
            if (request.CreatedFrom.HasValue)
            {
                query = query.Where(x => x.CREATED_TIME >= request.CreatedFrom.Value);
            }
            if (request.CreatedTo.HasValue)
            {
                query = query.Where(x => x.CREATED_TIME <= request.CreatedTo.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x => x.CODE.Contains(request.Keyword) || x.NOTE.Contains(request.Keyword));
            }
            query = query
                .OrderBy(x => x.STATUS)
                .ThenBy(x => x.CREATED_TIME)
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit);
            //var uncheckedOrder = query.Where(x => x.STATUS == OrderStatus.WaitingSellerConfirm)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var checkedOrder = query.Where(x => x.STATUS == OrderStatus.SellerConfirmed)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var preparingOrder = query.Where(x => x.STATUS == OrderStatus.Preparing)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var shippingOrder = query.Where(x => x.STATUS == OrderStatus.Shipping)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var deliveredOrder = query.Where(x => x.STATUS == OrderStatus.Shipped)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var completedOrder = query.Where(x => x.STATUS == OrderStatus.Received)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var rejectedOrder = query.Where(x => x.STATUS == OrderStatus.RejectedBySeller)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);
            //var canceledOrder = query.Where(x => x.STATUS == OrderStatus.CancelledByCustomer)
            //    .Skip((request.Page - 1) * request.Limit)
            //    .Take(request.Limit);

            var result = await query
                //.Union(checkedOrder)
                //.Union(preparingOrder)
                //.Union(shippingOrder)
                //.Union(deliveredOrder)
                //.Union(completedOrder)
                //.Union(rejectedOrder)
                //.Union(canceledOrder)
                .Select(order => new OrderDTO
                {
                    OrderId = order.ID,
                    OrderCode = order.CODE,
                    CreatedAt = order.CREATED_TIME,
                    CreatedBy = order.CREATED_BY,
                    AddressId = order.ADDRESS_ID,
                    AddressDescription = order.ADDRESS_DESCRIPTION,
                    UserId = order.USER_ID,
                    TotalPrice = order.TOTAL_PRICE,
                    TotalQuantity = order.TOTAL_QUANTITY,
                    Note = order.NOTE,
                    Status = order.STATUS,
                    ReceiverName = order.RECEIVER_NAME,
                    ReceiverPhone = order.RECEIVER_PHONE,
                    ListProducts = order.OrderProducts.Select(x => new ProductDTO
                    {
                        Code = x.ProductInfo.CODE,
                        Name = x.ProductInfo.NAME,
                        Id = x.PRODUCT_ID,
                        Quantity = x.QUANTITY,
                        Price = x.PRICE,
                        
                    }).ToList(),
                    

                    //TotalWeight = order.TOTAL_WEIGHT,
                })
                //.Union()
                .ToListAsync();
            return Result.Success(result);
        }
    }
}
