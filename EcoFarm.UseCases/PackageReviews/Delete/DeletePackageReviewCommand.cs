using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.PackageReviews.Delete
{
    public class DeletePackageReviewCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public DeletePackageReviewCommand(string id)
        {
            Id = id;
        }
        public DeletePackageReviewCommand()
        {

        }
    }

    internal class DeletePackageReviewHandler : ICommandHandler<DeletePackageReviewCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILocalizeService _localizeService;
        private readonly IAuthService _authService;
        public DeletePackageReviewHandler(IUnitOfWork unitOfWork, ILocalizeService localizeService,
            IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _localizeService = localizeService;
            _authService = authService;
        }

        public async Task<Result<bool>> Handle(DeletePackageReviewCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (accountType != EFX.AccountTypes.Customer)
            {
                return Result.Forbidden();
            }
            var review = await _unitOfWork.PackageReviews.FindAsync(request.Id);
            if (review is null)
            {
                return Result.NotFound(_localizeService.GetMessage(LocalizationEnum.PackageReviewNotFound));
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
            _unitOfWork.FarmingPackages.Update(package);
            _unitOfWork.PackageReviews.Remove(review);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
