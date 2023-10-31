using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Application.Features.ServicePackageFeatures.Commands.Approve
{
    public class ApproveServiceCommand : ICommand<bool>
    {
        public string ServiceId { get; set; }
    }

    internal class ApproveServiceHandler : ICommandHandler<ApproveServiceCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApproveServiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ApproveServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _unitOfWork.ServicePackages
                .GetQueryable()
                .Where(x => x.ID.Equals(request.ServiceId))
                .FirstOrDefaultAsync();
            if (service is not null)
            {
                service.STATUS = ServicePackageApprovalStatus.Accepted;
                _unitOfWork.ServicePackages.Update(service);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            throw new ValidationException("Không tìm thấy dịch vụ tương ứng");
        }
    }
}
