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

namespace EcoFarm.UseCases.FarmingPackages.Get
{
    public class GetListMyRegisteredPackageQuery : IQuery<FarmingPackageDTO>
    {
        public string Keyword { get; set; }
        public string EnterpriseId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFinished { get; set; }

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;

    }

    internal class GetListMyRegiteredPackageHandler : IQueryHandler<GetListMyRegisteredPackageQuery, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetListMyRegiteredPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<List<FarmingPackageDTO>>> Handle(GetListMyRegisteredPackageQuery request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (string.Compare(accountType, EFX.AccountTypes.Customer) != 0)
            {
                return Result.Forbidden();
            }
            var userId = _authService.GetAccountEntityId();
            var temp = _unitOfWork.UserRegisterPackages
                .GetQueryable()
                .Include(x => x.PackageInfo)
                .Include(x => x.PackageInfo.Enterprise)
                .Where(x => x.USER_ID.Equals(userId));
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                temp = temp.Where(x => x.PackageInfo.NAME.Contains(request.Keyword));
            }
            if (!string.IsNullOrEmpty(request.EnterpriseId))
            {
                temp = temp.Where(x => x.PackageInfo.SELLER_ENTERPRISE_ID.Equals(request.EnterpriseId));
            }
            if (request.IsActive.HasValue)
            {
                temp = temp.Where(x => x.PackageInfo.IS_ACTIVE == request.IsActive.Value);
            }
            if (request.IsFinished.HasValue )
            {
                if (request.IsFinished.Value)
                {
                    temp = temp.Where(x => !x.PackageInfo.END_TIME.HasValue);
                }
                else
                {
                    temp = temp.Where(x => x.PackageInfo.END_TIME.HasValue);
                }
            }
            var result = await temp
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .Select(x => new FarmingPackageDTO
                {
                    Id = x.PACKAGE_ID,
                    Code = x.PackageInfo.CODE,
                    Name = x.PackageInfo.NAME,
                    Description = x.PackageInfo.DESCRIPTION,
                    SellerEnterpriseId = x.PackageInfo.SELLER_ENTERPRISE_ID,
                    SellerEnterpriseName = x.PackageInfo.Enterprise.NAME,
                    CloseRegisterTime = x.PackageInfo.CLOSE_REGISTER_TIME,
                    StartTime = x.PackageInfo.START_TIME,
                    EndTime = x.PackageInfo.END_TIME,
                    AverageRating = x.PackageInfo.AverageRating,
                    ServicePackageApprovalStatus = x.PackageInfo.STATUS,
                    Price = x.PRICE,
                    Currency = x.CURRENCY,
                    EstimatedStartTime = x.PackageInfo.ESTIMATED_START_TIME,
                    EstimatedEndTime = x.PackageInfo.ESTIMATED_END_TIME,
                    NumbersOfRating = x.PackageInfo.NUMBERS_OF_RATING,
                    PackageType = x.PackageInfo.PACKAGE_TYPE,
                    
                    CreatedTime = x.PackageInfo.CREATED_TIME,

                }).ToListAsync();
            return Result.Success(result);
        }
    }
}
