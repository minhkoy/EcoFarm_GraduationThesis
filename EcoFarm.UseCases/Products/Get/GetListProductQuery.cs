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
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Products.GetList
{
    public class GetListProductQuery : IQuery<ProductDTO>
    {
        /// <summary>
        /// Keyword for Code & Name
        /// </summary>
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
        public string PackageId { get; set; }
        public int? MinimumQuantity { get; set; }
        public int? MaximumQuantity { get; set; }
        public decimal? MinimumPrice { get; set; }
        public decimal? MaximumPrice { get; set; }
        public CurrencyType? Currency { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
    
    public class GetListProductQueryHandler : IQueryHandler<GetListProductQuery, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListProductQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Products
                .GetQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.CODE.Contains(request.Keyword));
            }
            if (request.IsActive.HasValue)
            {
                query = query.Where(x => x.IS_ACTIVE == request.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(request.PackageId))
            {
                query = query.Where(x => x.PACKAGE_ID == request.PackageId);
            }
            if (request.MinimumQuantity.HasValue)
            {
                query = query.Where(x => x.QUANTITY >= request.MinimumQuantity.Value);
            }
            if (request.MaximumQuantity.HasValue)
            {
                query = query.Where(x => x.QUANTITY <= request.MaximumQuantity.Value);
            }
            if (request.MinimumPrice.HasValue)
            {
                query = query.Where(x => x.PRICE >= request.MinimumPrice.Value);
            }
            if (request.MaximumPrice.HasValue)
            {
                query = query.Where(x => x.PRICE <= request.MaximumPrice.Value);
            }
            if (request.Currency.HasValue)
            {
                query = query.Where(x => x.CURRENCY == request.Currency.Value);
            }
            if (!request.PageSize.HasValue)
            {
                request.PageSize = EFX.DefaultPageSize;
            }
            if (!request.Page.HasValue)
            {
                request.Page = 1;
            }
            var result = await query
                .OrderByDescending(x => x.CREATED_TIME)
                .Skip((request.Page.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .Select(x => new ProductDTO
                {
                    Id = x.ID,
                    Code = x.CODE,
                    Name = x.NAME,
                    Description = x.DESCRIPTION,
                    PackageId = x.PACKAGE_ID,
                    Quantity = x.QUANTITY,
                    Sold = x.SOLD,
                    Price = x.PRICE,
                    PriceForRegistered = x.PRICE_FOR_REGISTERED,
                    Currency = x.CURRENCY,
                    CreatedTime = x.CREATED_TIME,
                })
                .ToListAsync();
            return Result<List<ProductDTO>>.Success(result);
        }
    }
}
