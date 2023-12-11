using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.PackageReviews.Get
{
    public class GetListReviewCommand : IQuery<ReviewDTO>
    {
        public string PackageId { get; set; }
        public int? Rating { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class GetListReviewHandler : IQueryHandler<GetListReviewCommand, ReviewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizeService _localizeService;
        private readonly IAuthService _authService;
        public GetListReviewHandler(IUnitOfWork unitOfWork, ILocalizeService localizeService,
                       IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _localizeService = localizeService;
            _authService = authService;
        }

        public async Task<Result<List<ReviewDTO>>> Handle(GetListReviewCommand request, CancellationToken cancellationToken)
        {
            IQueryable<UserPackageReview> query = _unitOfWork.PackageReviews
                .GetQueryable()
                .Include(x => x.UserInfo);
            if (!string.IsNullOrEmpty(request.PackageId))
            {
                query = query.Where(x => string.Equals(x.PACKAGE_ID, request.PackageId));
            }
            //else
            //{
            //    if (!string.Equals(_authService.GetAccountTypeName(), EFX.AccountTypes.Admin)
            //        || !string.Equals(_authService.GetAccountTypeName(), EFX.AccountTypes.SuperAdmin))
            //    {
            //        return Result.Forbidden();
            //    }
            //}
            if (request.Rating.HasValue)
            {
                if (request.Rating.Value < -1 || request.Rating.Value > 5)
                {
                    return Result.Error(_localizeService.GetMessage(LocalizationEnum.RatingInvalid));
                }
                if (request.Rating.Value == 0)
                {
                    query = query.Where(x => !x.RATING.HasValue);
                }
                else
                {
                    query = query.Where(x => x.RATING == request.Rating.Value);
                }
            }
            var result = await query
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .Select(x => new ReviewDTO
                {
                    Content = x.COMMENT,
                    Rating = x.RATING,
                    CreatedAt = x.CREATED_TIME,
                    ModifiedAt = x.MODIFIED_TIME,
                    ReviewId = x.ID,
                    PackageId = x.PACKAGE_ID,
                    UserId = x.USER_ID,
                    UserFullname = x.UserInfo.NAME,

                })
                .ToListAsync();
            return Result.Success(result);
        }
    }

}
