using EcoFarm.Application.Features.Administration.AccountManagerFeatures.Commands.LockAccount;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Lock
{
    public class LockAccountValidator : AbstractValidator<LockAccountCommand>
    {
        public LockAccountValidator()
        {
            RuleFor(x => string.Concat(x.Id, x.Username)).NotEmpty().WithMessage("Thông tin người dùng không được để trống");
            RuleFor(x => x.Reason).NotEmpty().WithMessage("Lý do khóa không được để trống");
        }
    }
}
