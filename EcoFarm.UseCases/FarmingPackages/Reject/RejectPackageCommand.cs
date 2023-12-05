using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingPackages.Reject
{
    public class RejectPackageCommand : ICommand<bool>
    {
        public string PackageId { get; set; }
        public string Reason { get; set; }
    }
    internal class RejectPackageHandler : ICommandHandler<RejectPackageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public RejectPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(RejectPackageCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null)
            {
                return Result.Unauthorized();
            }
            if (!account.IS_ACTIVE)
            {
                return Result.Error("Tài khoản bị khóa");
            }
            if (!account.IS_EMAIL_CONFIRMED)
            {
                return Result.Error("Tài khoản chưa được xác thực email");
            }
            if (account.ACCOUNT_TYPE != AccountType.Admin)
            {
                return Result.Forbidden();
            }
            var service = await _unitOfWork.FarmingPackages.FindAsync(request.PackageId);
            if (service is null)
            {
                return Result.Error("Không tìm thấy gói farming tương ứng");             
            }
            if (service.STATUS != ServicePackageApprovalStatus.Pending)
            {
                return Result.Error("Gói farming đã được duyệt hoặc bị từ chối. Vui lòng kiểm tra lại");
            }
            service.STATUS = ServicePackageApprovalStatus.Rejected;
            service.REJECT_REASON = request.Reason;
            _unitOfWork.FarmingPackages.Update(service);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Từ chối gói farming thành công");
        }
    }
}
