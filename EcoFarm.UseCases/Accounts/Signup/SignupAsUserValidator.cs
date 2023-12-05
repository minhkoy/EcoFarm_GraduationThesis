using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Signup
{
    public class SignupAsUserValidator : AbstractValidator<SignupAsUserCommand>
    {
        public SignupAsUserValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                //.NotEmpty().WithMessage("Tên không được để trống")
                .MaximumLength(60).WithMessage("Tên không được quá 60 ký tự");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .MaximumLength(20).WithMessage("Tên đăng nhập không được quá 20 ký tự");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$").WithMessage("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số");

        }
    }
}
