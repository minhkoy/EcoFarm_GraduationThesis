using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Tasks.ServicePackageFeatures.Commands.Delete
{
    public class DeleteServiceValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Không có thông tin dịch vụ cần xóa");
        }
    }
}
