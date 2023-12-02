using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Orders.Get
{
    public class GetOrderQuery : IQuerySingle<OrderDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }

    internal class GetOrderHandler : IQuerySingleHandler<GetOrderQuery, OrderDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetOrderHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<OrderDTO>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _unitOfWork.Orders
                .GetQueryable()
                .Include(x => x.EnterpriseInfo)
                .Include(x => x.AddressInfo);
            if (!string.IsNullOrEmpty(request.Id))
            {
                var order = await query.FirstOrDefaultAsync(x => string.Equals(request.Id, x.ID));
                if (order is null)
                {
                    return Result.NotFound();
                }
                return Result.Success(new OrderDTO
                {
                    Id = order.ID,
                    Code = order.CODE,
                    CreatedAt = order.CREATED_TIME,
                    AddressId = order.ADDRESS_ID,
                    AddressDescription = order.ADDRESS_DESCRIPTION,
                    TotalPrice = order.TOTAL_PRICE,
                    SellerEnterpriseId = order.ENTERPRISE_ID,
                    SellerEnterpriseName = order.EnterpriseInfo?.NAME,


                });
            }
            else
            {
                var order = await query
                    .FirstOrDefaultAsync(x => string.Equals(x.CODE, request.Code));
                if (order is null)
                {
                    return Result.NotFound();
                }
                return Result.Success(new OrderDTO
                {
                    Id = order.ID,
                    Code = order.CODE,
                    CreatedAt = order.CREATED_TIME,
                    AddressId = order.ADDRESS_ID,
                    AddressDescription = order.ADDRESS_DESCRIPTION,
                    TotalPrice = order.TOTAL_PRICE,
                    SellerEnterpriseId = order.ENTERPRISE_ID,
                    SellerEnterpriseName = order.EnterpriseInfo.NAME,
                    Name = order.NAME,
                    UserId = order.USER_ID,
                    Note = order.NOTE,
                    TotalQuantity = order.TOTAL_QUANTITY,
                    PaymentMethod = order.PAYMENT_METHOD,
                    Status = order.STATUS,



                });
            }
        }
    }
}
