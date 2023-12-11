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
        public string UserId { get; set; }
        public OrderStatus? Status { get; set; }
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
            if (!string.Equals(accountType, EFX.AccountTypes.Seller))
            {
                return Result.Forbidden();
            }
            var query = _unitOfWork.Orders
                .GetQueryable()
                .Where(x => string.Equals(x.ENTERPRISE_ID, _authService.GetAccountEntityId()));              
            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                query = query.Where(x => string.Equals(x.USER_ID, request.UserId));
            }
            if (request.Status.HasValue)
            {
                query = query.Where(x => x.STATUS == request.Status.Value);
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
            var result = await query
                .Select(order => new OrderDTO
                {
                    OrderId = order.ID,
                    OrderCode = order.CODE,
                    CreatedAt = order.CREATED_TIME,
                    AddressId = order.ADDRESS_ID,
                    AddressDescription = order.ADDRESS_DESCRIPTION,
                    TotalPrice = order.TOTAL_PRICE,

                })
                .ToListAsync();
            return Result.Success(result);
        }
    }
}
