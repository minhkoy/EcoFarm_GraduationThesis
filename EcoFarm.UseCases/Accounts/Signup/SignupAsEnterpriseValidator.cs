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
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
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
