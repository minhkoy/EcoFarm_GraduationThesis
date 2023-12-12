using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
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

namespace EcoFarm.UseCases.PackageReviews.Create
{
    public class CreatePackageReviewCommand : ICommand<ReviewDTO>
    {
        public string PackageId { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }
    }

    internal class CreatePackageReviewHandler : ICommandHandler<CreatePackageReviewCommand, ReviewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        //private readonly IHubContext
        public CreatePackageReviewHandler(IUnitOfWork unitOfWork, IAuthService authService, ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<ReviewDTO>> Handle(CreatePackageReviewCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null || account.IS_DELETE)
            {
                return Result.Unauthorized();
            }
            if (!account.IS_ACTIVE)
            {
                return Result.Error("Tài khoản đã bị khóa. Vui lòng thử lại sau.");
            }
            if (account.ACCOUNT_TYPE != AccountType.Customer)
            {
                return Result.Forbidden();
            }
            var package = await _unitOfWork.FarmingPackages
                .FindAsync(request.PackageId);
            if (package is null)
            {
                return Result.NotFound(_localizeService.GetMessage(LocalizationEnum.PackageNotFound));
            }
            if (!package.IS_ACTIVE)
            {
                return Result.Error("Gói farming tạm thời bị khóa. Vui lòng thử lại sau");
            }
            var user = await _unitOfWork.Users
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(account.ID));
            if (user is null)
            {
                return Result.Forbidden();
            }

            var packageReview = await _unitOfWork.PackageReviews
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.PACKAGE_ID.Equals(package.ID) && x.USER_ID.Equals(user.ID));
            if (packageReview is not null)
            {
                return Result.Error(_localizeService.GetMessage(LocalizationEnum.PackageReviewed));
            }

            var review = new UserPackageReview
            {
                ID = Guid.NewGuid().ToString(),
                USER_ID = user.ID,
                PACKAGE_ID = package.ID,
                COMMENT = request.Content,
                RATING = request.Rating,
            };

            if (request.Rating.HasValue && request.Rating.Value >= 1 && request.Rating.Value <= 5)
            {
                package.NUMBERS_OF_RATING++;
                package.TOTAL_RATING_POINTS += request.Rating.Value;
            }

            _unitOfWork.FarmingPackages.Update(package);
            _unitOfWork.PackageReviews.Add(review);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(new ReviewDTO
            {
                ReviewId = review.ID,
                PackageId = review.PACKAGE_ID,
                UserId = account.ID,
                Username = account.USERNAME,
                UserFullname = account.NAME,
                //EnterpriseId = account.ID, XXX
                Content = review.COMMENT,
                Rating = review.RATING,
                CreatedAt = review.CREATED_TIME,
            }, "Thêm mới đánh giá thành công");
        }
    }
}
