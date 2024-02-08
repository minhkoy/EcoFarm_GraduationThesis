using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using EcoFarm.UseCases.DTOs.Stats;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Statstics.GetEnterpriseStats
{
    public class GetEnterpriseStatQuery : IQuerySingle<EnterpriseStatDTO>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public GetEnterpriseStatQuery() { }
    }

    internal class Handler : IQuerySingleHandler<GetEnterpriseStatQuery, EnterpriseStatDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<EnterpriseStatDTO>> Handle(GetEnterpriseStatQuery request, CancellationToken cancellationToken)
        {
            if (!request.FromDate.HasValue)
            {
                request.FromDate = DateTime.Now.AddDays(-7);
            }
            if (!request.ToDate.HasValue)
            {
                request.ToDate = DateTime.Now;
            }
            if (_authService.GetAccountTypeName() != EFX.AccountTypes.Seller)
                return Result.Forbidden();
            var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(_authService.GetAccountEntityId());
            if (enterprise is null || !enterprise.IS_ACTIVE)
            {
                return Result.Error("Không có thông tin doanh nghiệp, hoặc tài khoản doanh nghiệp đang bị khóa");
            }
            var result = new EnterpriseStatDTO
            {
                EnterpriseId = enterprise.ID,
                EnterpriseName = enterprise.NAME,
                TotalRegisterPackageInTimeRange = 0,
                TotalSoldProductInTimeRange = 0,
                TotalOrderPriceInTimeRange = 0,
                TotalOrderPriceSoFar = 0,
                TopRegisteredPackagesInTimeRange = new List<FarmingPackageDTO>(),
                TopSoldProductsInTimeRange = new List<ProductDTO>()
            };           
            var tempPackageQuery = _unitOfWork.FarmingPackages.GetQueryable()
                .Where(x => x.SELLER_ENTERPRISE_ID == enterprise.ID && x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate);
            result.TotalRegisterPackageInTimeRange = _unitOfWork
                .UserRegisterPackages.GetQueryable()
                .Include(x => x.PackageInfo)
                .Where(x => string.Equals(x.PackageInfo.SELLER_ENTERPRISE_ID, enterprise.ID) && x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate)
                .LongCount();
            result.TotalSoldProductInTimeRange = _unitOfWork
                .OrderProducts.GetQueryable()
                .Include(x => x.ProductInfo)
                .Where(x => string.Equals(x.ProductInfo.ENTERPRISE_ID, enterprise.ID) && x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate)
                .Sum(x => x.QUANTITY);
            result.TotalOrderPriceInTimeRange = _unitOfWork
                .Orders
                .GetQueryable()
                .Where(x => string.Equals(x.ENTERPRISE_ID, enterprise.ID) && x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate)
                .Sum(x => x.TOTAL_PRICE);
            result.TotalOrderPriceSoFar = _unitOfWork
                .Orders.GetQueryable()
                .Where(x => string.Equals(x.ENTERPRISE_ID, enterprise.ID))
                .Sum(x => x.TOTAL_PRICE);
            result.TopRegisteredPackagesInTimeRange = _unitOfWork
                .FarmingPackages
                .GetQueryable()
                .Include(x => x.UserRegisterPackages)
                .Where(x => string.Equals(x.SELLER_ENTERPRISE_ID, enterprise.ID))
                .OrderByDescending(x => x.UserRegisterPackages.Count(x => x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate))
                .Take(EFX.DefaultPageSize)
                .Select(x => new FarmingPackageDTO
                {
                    Id = x.ID,
                    Name = x.NAME,
                    Code = x.CODE,
                    QuantityRegistered = x.UserRegisterPackages.Count(x => x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate)
                })
                .ToList();
            result.TopSoldProductsInTimeRange = await _unitOfWork
                .Products.GetQueryable()
                .Include(x => x.OrderProducts)
                .ThenInclude(x => x.OrderInfo)
                .Where(x => string.Equals(x.ENTERPRISE_ID, enterprise.ID) && x.CREATED_TIME >= request.FromDate && x.CREATED_TIME <= request.ToDate)
                .OrderByDescending(x =>
                    x.OrderProducts
                    .Where(x => x.OrderInfo.STATUS == OrderStatus.Received)
                    .Sum(x => x.QUANTITY))
                .Take(EFX.DefaultPageSize)
                .Select(x => new ProductDTO
                {
                    Id = x.ID,
                    Code = x.CODE,
                    Name = x.NAME,
                    Quantity = x.OrderProducts.Where(x => x.OrderInfo.STATUS == OrderStatus.Received)
                    .Sum(x => x.QUANTITY)
                })
                .ToListAsync();
            return Result.Success(result);
        }
    }
}
