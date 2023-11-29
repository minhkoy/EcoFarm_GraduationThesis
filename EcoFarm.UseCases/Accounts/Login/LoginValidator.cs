using EcoFarm.Application.Localization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Login
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty()
                .WithName("USERNAME")
                .WithMessage("Cần nhập thông tin tên đăng nhập hoặc email.");
            RuleFor(x => x.Password).NotEmpty()
                .WithName("Password")
                .WithMessage("Cần nhập mật khẩu.");
        }
    }
}
