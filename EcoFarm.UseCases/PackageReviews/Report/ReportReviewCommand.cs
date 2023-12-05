using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.PackageReviews.Report
{
    public class ReportReviewCommand : ICommand<bool>
    {
        public string ReviewId { get; set; }
        public string Reason { get; set; }
    }

    internal class ReportReviewCommandHandler : ICommandHandler<ReportReviewCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public ReportReviewCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(ReportReviewCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefault(x => string.Equals(x.USERNAME, username));
            if (account is null || account.IS_DELETE)
            {
                return Result.Unauthorized();
            }
            if (!account.IS_ACTIVE || !account.IS_EMAIL_CONFIRMED)
            {
                return Result.Forbidden();
            }
            var review = await _unitOfWork.PackageReviews
                .FindAsync(request.ReviewId);
            if (review is null)
            {
                return Result.NotFound("Không tìm thấy đánh giá cần được báo cáo");
            }
            Notification notification = new()
            {
                FROM_ACCOUNT_ID = account.ID,
                //TO_ACCOUNT_ID = "",
                FROM_ACCOUNT_TYPE = account.ACCOUNT_TYPE,
                //TO_ACCOUNT_TYPE = AccountType.Admin,
                CONTENT = request.Reason,

                OBJECT_TYPE = NotificationObjectType.PackageReview,
                ACTION_TYPE = NotificationActionType.Report,
            };
            NotificationAccount notificationAccount = new()
            {
                TO_ACCOUNT_ID = account.ID,
                TO_ACCOUNT_TYPE = AccountType.Admin,
                NOTIFICATION_ID = notification.ID,
            };
            _unitOfWork.Notifications.Add(notification);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Báo cáo đánh giá thành công!");
        }
    }
}
