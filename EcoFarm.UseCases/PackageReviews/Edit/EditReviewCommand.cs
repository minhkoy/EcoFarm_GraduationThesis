using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.PackageReviews.Edit
{
    public class EditReviewCommand : ICommand<ReviewDTO>
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int? Rating { get; set; }
    }

    internal class EditReviewHandler : ICommandHandler<EditReviewCommand, ReviewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public EditReviewHandler(IUnitOfWork unitOfWork, IAuthService authService, ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<ReviewDTO>> Handle(EditReviewCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (accountType != EFX.AccountTypes.Customer)
            {
                return Result.Forbidden();
            }
            var review = await _unitOfWork.PackageReviews.FindAsync(request.Id);
            if (review is null)
            {
                return Result.NotFound();
            }
            var userId = _authService.GetAccountEntityId();
            if (!string.Equals(review.USER_ID, userId))
            {
                return Result.Forbidden();
            }
            var package = await _unitOfWork.FarmingPackages.FindAsync(review.PACKAGE_ID);
            if (package is null)
            {
                return Result.Error(_localizeService.GetMessage(LocalizationEnum.PackageNotFound));
            }
            //If old rating has point, then remove it
            if (review.RATING.HasValue)
            {
                package.TOTAL_RATING_POINTS -= review.RATING.Value;
                package.NUMBERS_OF_RATING--;

            }
            if (request.Rating.HasValue)
            {
                package.TOTAL_RATING_POINTS += review.RATING.Value;
                package.NUMBERS_OF_RATING++;
            }
            review.COMMENT = request.Content;
            review.RATING = request.Rating;

            _unitOfWork.FarmingPackages.Update(package);
            _unitOfWork.PackageReviews.Update(review);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(new ReviewDTO
            {
                ReviewId = review.ID,
                Content = review.COMMENT,
                Rating = review.RATING,
                CreatedAt = review.CREATED_TIME,
                ModifiedAt = review.MODIFIED_TIME,
                PackageId = review.PACKAGE_ID,
                EnterpriseId = package.SELLER_ENTERPRISE_ID,
                UserId = review.USER_ID,
                Username = _authService.GetUsername(),
                UserFullname = _authService.GetFullname()
            });
        }
    }
}
