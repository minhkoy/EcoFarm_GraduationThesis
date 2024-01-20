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

namespace EcoFarm.UseCases.Products.Get
{
    public class GetListProductQuery : IQuery<ProductDTO>
    {
        public string EnterpriseId { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }

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
        public int? Limit { get; set; }
    }

    public class GetListProductQueryHandler : IQueryHandler<GetListProductQuery, ProductDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetListProductQueryHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<List<ProductDTO>>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _unitOfWork.Products
                .GetQueryable()
                .Include(x => x.Package)
                .Include(x => x.Enterprise)
                .Include(x => x.ProductMedias);
            if (!string.IsNullOrEmpty(request.Id) || !string.IsNullOrEmpty(request.Code))
            {
                Product product = null;
                if (!string.IsNullOrEmpty(request.Id))
                {
                    product = await query.FirstOrDefaultAsync(x => string.Equals(request.Id, x.ID));
                }
                else
                {
                    product = await query.FirstOrDefaultAsync(x => string.Equals(request.Code, x.CODE));
                }

                if (product is null)
                {
                    return Result<List<ProductDTO>>.Success(new List<ProductDTO>());
                }

                var accountType = _authService.GetAccountTypeName();
                bool? isRegisteredPackage = null;
                if (string.Equals(accountType, EFX.AccountTypes.Customer) && !string.IsNullOrEmpty(product.PACKAGE_ID))
                {
                    isRegisteredPackage = await _unitOfWork.UserRegisterPackages
                    .GetQueryable()
                    .AnyAsync(x => string.Equals(x.USER_ID, _authService.GetAccountEntityId()) && string.Equals(x.PACKAGE_ID, product.PACKAGE_ID));

                }
                
                var rs = new ProductDTO
                {
                    Id = product.ID,
                    Code = product.CODE,
                    Name = product.NAME,
                    Description = product.DESCRIPTION,
                    PackageId = product.PACKAGE_ID,
                    Quantity = product.QUANTITY,
                    Sold = product.SOLD,
                    QuantityRemain = product.CURRENT_QUANTITY,
                    PackageCode = product.Package?.CODE,
                    PackageName = product.Package?.NAME,
                    IsUserRegisteredPackage = isRegisteredPackage,
                    Price = product.PRICE,
                    PriceForRegistered = product.PRICE_FOR_REGISTERED,
                    Currency = product.CURRENCY,
                    CreatedTime = product.CREATED_TIME,
                    SellerEnterpriseId = product.ENTERPRISE_ID,
                    SellerEnterpriseName = product.Enterprise.NAME,
                    Medias = product.ProductMedias?.Select(x => new ProductMediaDTO
                    {
                        ImageUrl = x.MEDIA_URL,
                    }).ToList(),
                };
                return Result.Success(new List<ProductDTO>{ rs });
            }
            if (!string.IsNullOrEmpty(request.EnterpriseId))
            {
                query = query.Where(x => string.Equals(x.ENTERPRISE_ID, request.EnterpriseId));
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.CODE.Contains(request.Keyword) || x.NAME.Contains(request.Keyword));
            }
            if (request.IsActive.HasValue)
            {
                query = query.Where(x => x.IS_ACTIVE == request.IsActive.Value);
            }
            if (!string.IsNullOrEmpty(request.PackageId))
            {
                query = query.Where(x => x.PACKAGE_ID == request.PackageId);
            }
            if (request.MinimumQuantity.HasValue && request.MinimumQuantity.Value > 0)
            {
                query = query.Where(x => x.CURRENT_QUANTITY >= request.MinimumQuantity.Value);
            }
            if (request.MaximumQuantity.HasValue && request.MaximumQuantity.Value > 0)
            {
                query = query.Where(x => x.CURRENT_QUANTITY <= request.MaximumQuantity.Value);
            }
            if (request.MinimumPrice.HasValue && request.MinimumPrice.Value > 0)
            {
                query = query.Where(x => x.PRICE >= request.MinimumPrice.Value);
            }
            if (request.MaximumPrice.HasValue && request.MaximumPrice.Value > 0)
            {
                query = query.Where(x => x.PRICE <= request.MaximumPrice.Value);
            }
            if (request.Currency.HasValue)
            {
                query = query.Where(x => x.CURRENCY == request.Currency.Value);
            }
            if (!request.Limit.HasValue)
            {
                request.Limit = EFX.DefaultPageSize;
            }
            if (!request.Page.HasValue)
            {
                request.Page = 1;
            }
            var result = await query
                .OrderByDescending(x => x.CREATED_TIME)
                .Skip((request.Page.Value - 1) * request.Limit.Value)
                .Take(request.Limit.Value)
                .Select(x => new ProductDTO
                {
                    Id = x.ID,
                    Code = x.CODE,
                    Name = x.NAME,
                    Description = x.DESCRIPTION,
                    PackageId = x.PACKAGE_ID,
                    Quantity = x.QUANTITY,
                    Sold = x.SOLD,
                    QuantityRemain = x.CURRENT_QUANTITY,
                    PackageCode = x.Package.CODE,
                    PackageName = x.Package.NAME,
                    Price = x.PRICE,
                    PriceForRegistered = x.PRICE_FOR_REGISTERED,
                    Currency = x.CURRENCY,
                    CreatedTime = x.CREATED_TIME,
                    SellerEnterpriseId = x.ENTERPRISE_ID,
                    SellerEnterpriseName = x.Enterprise.NAME,
                    Medias = x.ProductMedias.Select(x => new ProductMediaDTO
                    {
                        ImageUrl = x.MEDIA_URL,
                    }).ToList(),
                })
                .ToListAsync();
            return Result<List<ProductDTO>>.Success(result);
        }
    }
}
