using EcoFarm.Application.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Administration.AuthenticationFeatures.Commands.ForgetPassword
{
    public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ForgetPasswordValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => $"{x.Username.Trim()}{x.Email.Trim()}")
                .MinimumLength(1)
                //.WithErrorCode("VAL")
                .WithMessage("Vui lòng điền thông tin yêu cầu");
            RuleFor(x => x.Username.Trim().Length * x.Email.Trim().Length)
                .LessThan(1)
                .WithMessage("Không thể cùng khôi phục bằng cả thông tin tên đăng nhập và email");
            //Cần cân nhắc. Tùy theo yêu cầu có thể validate hoặc bỏ.
            //Under consideration.
            //These following fields can be validated or not, which depends on the requirements
            RuleFor(x => x.Username)
                .Must(IsExistUsername)
                .WithMessage("Tên đăng nhập không tồn tại trong hệ thống. Vui lòng kiểm tra lại");
            RuleFor(x => x.Email)
                .Must(IsExistEmail)
                .WithMessage("Email không tồn tại trong hệ thống. Vui lòng kiểm tra lại");
        }

        private bool IsExistUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) { return true; }
            var existingUser = _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefault(x => string.Equals(x.USERNAME, username));
            if (existingUser is null)
            {
                return false;
            }
            return true;
        }

        private bool IsExistEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { return true; }
            var existingUser = _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefault(x => x.EMAIL.Equals(email));
            if (existingUser is null)
            {
                return false;
            }
            return true;
        }
    }
}
