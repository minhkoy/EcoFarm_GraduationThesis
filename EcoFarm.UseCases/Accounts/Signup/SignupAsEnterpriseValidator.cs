using EcoFarm.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Signup
{
    public class SignupAsEnterpriseValidator : AbstractValidator<SignupAsEnterpriseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SignupAsEnterpriseValidator(IUnitOfWork unitOfWork)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .Must(NotExistUsername).WithMessage("Tên đăng nhập đã tồn tại");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$").WithMessage("Mật khẩu phải có ít nhất 8 ký tự, bao gồm chữ hoa, chữ thường và số");
            //RuleFor(x => x.Email)
            //    .NotEmpty().WithMessage("Email không được để trống")
            //    .Matches(Regex.);
        }

        public bool NotExistUsername(string username)
        {
            var existedAccount = _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefault(x => string.Equals(x.USERNAME, username));
            return (existedAccount is null);
        }
    }
}
