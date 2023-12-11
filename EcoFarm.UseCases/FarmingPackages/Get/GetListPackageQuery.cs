using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
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

namespace EcoFarm.UseCases.FarmingPackages.Get
{
    public class GetListPackageQuery : IQuery<FarmingPackageDTO>
    {
        public string EnterpriseId { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        /// <summary>
        /// Keyword for Package Code/ Name
        /// </summary>
        public string Keyword { get; set; }
        public bool? IsStarted { get; set; }
        public bool? IsEnded { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsCloseForRegistered { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class GetListPackageHandler : IQueryHandler<GetListPackageQuery, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListPackageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<FarmingPackageDTO>>> Handle(GetListPackageQuery request, CancellationToken cancellationToken)
        {
            var temp = _unitOfWork.FarmingPackages
                .GetQueryable();
            if (!string.IsNullOrEmpty(request.EnterpriseId))
            {
                temp = temp.Where(x => x.SELLER_ENTERPRISE_ID.Equals(request.EnterpriseId));
            }
            if (request.PriceFrom.HasValue && request.PriceFrom.Value > 0)
            { 
                temp = temp.Where(x => x.PRICE >= request.PriceFrom.Value);
            }
            if (request.PriceTo.HasValue && request.PriceTo.Value > 0)
            {
                temp = temp.Where(x => x.PRICE <= request.PriceTo.Value);
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                temp = temp.Where(x => x.CODE.Contains(request.Keyword) || x.NAME.Contains(request.Keyword));
            }
            if (request.IsStarted.HasValue && request.IsStarted.Value)
            {
                temp = temp.Where(x => x.START_TIME.HasValue);
            }
            if (request.IsEnded.HasValue && request.IsEnded.Value)
            {
                temp = temp.Where(x => x.END_TIME.HasValue);
            }
            if (request.IsActive.HasValue)
            {
                temp = temp.Where(x => x.IS_ACTIVE);
            }
            if (request.IsApproved.HasValue)
            {
                if (request.IsApproved.Value)
                {
                    temp = temp.Where(x => x.STATUS == ServicePackageApprovalStatus.Approved);
                }
                else
                {
                    temp = temp.Where(x => x.STATUS == ServicePackageApprovalStatus.Rejected);
                }
            }
            if (request.IsCloseForRegistered.HasValue && request.IsCloseForRegistered.Value)
            {
                temp = temp.Where(x => x.CLOSE_REGISTER_TIME.HasValue && x.CLOSE_REGISTER_TIME.Value <= DateTime.Now.ToVnDateTime());
            }
            var rs = await temp
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .Include(x => x.Enterprise)
                .Select(x => new FarmingPackageDTO
            {
                Id = x.ID,
                Code = x.CODE,
                Name = x.NAME,
                Description = x.DESCRIPTION,
                EstimatedStartTime = x.ESTIMATED_START_TIME,
                EstimatedEndTime = x.ESTIMATED_END_TIME,
                StartTime = x.START_TIME,
                EndTime = x.END_TIME,
                CloseRegisterTime = x.CLOSE_REGISTER_TIME,
                Enterprise = new EnterpriseDTO
                {
                    EnterpriseId = x.Enterprise.ID,
                    FullName = x.Enterprise.NAME,
                },
                Price = x.PRICE,
                Currency = x.CURRENCY,
                QuantityStart = x.QUANTITY_START,
                QuantityRegistered = x.QUANTITY_REGISTERED,
                QuantityRemain = x.QuantityRemain,
                RejectReason = x.REJECT_REASON,
                ServicePackageApprovalStatus = x.STATUS,
                PackageType = x.PACKAGE_TYPE,
                CreatedTime = x.CREATED_TIME,
                CreatedBy = x.CREATED_BY,
                NumbersOfRating = x.NUMBERS_OF_RATING,
                AverageRating = x.AverageRating,
                
            }).ToListAsync();
            return Result.Success(rs);
        }
    }
}
