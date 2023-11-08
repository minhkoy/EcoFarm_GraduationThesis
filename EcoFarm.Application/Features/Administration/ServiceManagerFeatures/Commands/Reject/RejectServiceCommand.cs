using EcoFarm.Application.Common.Results;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Reject
{
    public class RejectServiceCommand : ICommand<bool>
    {
        public string ServiceId { get; set; }
        public string RejectReason { get; set; }
    }

    internal class RejectServiceHandler : ICommandHandler<RejectServiceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RejectServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<bool>> Handle(RejectServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.ServicePackages
                .GetQueryable()
                .Where(x => x.ID.Equals(request.ServiceId))
                .FirstOrDefaultAsync();
            if (service is not null)
            {
                //XXXX: Need to check more, if the service has been approved or rejected, then we can't approve it again
                //And of course, we need to send a notification to the seller
                service.STATUS = ServicePackageApprovalStatus.Denied;
                service.REJECT_REASON = request.RejectReason;
                _unitOfWork.ServicePackages.Update(service);
                await _unitOfWork.SaveChangesAsync();
                return new OkResult<bool>(true);
            }
            throw new ValidationException("Không tìm thấy dịch vụ tương ứng");
        }
    }
}
