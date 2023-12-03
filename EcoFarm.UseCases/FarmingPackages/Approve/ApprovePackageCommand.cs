using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingPackages.Approve
{
    public class ApprovePackageCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public ApprovePackageCommand() { }
        public ApprovePackageCommand(string id)
        {
            Id = id;
        }

        public class ApprovePackageHandler : ICommandHandler<ApprovePackageCommand, bool>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAuthService _authService;
            //private readonly INotificationService _notificationService;
            public ApprovePackageHandler(IUnitOfWork unitOfWork, IAuthService authService)// INotificationService notificationService)
            {
                _unitOfWork = unitOfWork;
                _authService = authService;
                //_notificationService = notificationService;
            }
            public async Task<Result<bool>> Handle(ApprovePackageCommand request, CancellationToken cancellationToken)
            {
                //XXX: Validation not implemented. Just happy case.
                var package = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
                if (package == null)
                    return Result<bool>.NotFound("Không tìm thấy gói farming");
                package.APPROVE_OR_REJECT_TIME = DateTime.Now.ToVnDateTime();
                package.APPROVE_OR_REJECT_BY = _authService.GetUsername();
                package.STATUS = ServicePackageApprovalStatus.Approved;

                _unitOfWork.FarmingPackages.Update(package);
                await _unitOfWork.SaveChangesAsync();
                //await _notificationService.SendNotificationAsync(package.ENTERPRISE_ID, $"Gói farming {package.NAME} đã được duyệt");
                return Result.SuccessWithMessage("Duyệt gói farming thành công");
            }
        }
    }
}
