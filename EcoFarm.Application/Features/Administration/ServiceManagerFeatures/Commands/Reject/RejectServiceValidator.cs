using EcoFarm.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.ServiceManagerFeatures.Commands.Reject
{
    public class RejectServiceValidator : AbstractValidator<RejectServiceCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RejectServiceValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.ServiceId).NotEmpty().WithMessage("Vui lòng cung cấp ID dịch vụ");
            RuleFor(x => x.RejectReason)
                .NotEmpty().WithMessage("Vui lòng cung cấp lý do từ chối")
                .MinimumLength(10).WithMessage("Lý do từ chối phải có ít nhất 10 ký tự");
            
        }
    }
}
