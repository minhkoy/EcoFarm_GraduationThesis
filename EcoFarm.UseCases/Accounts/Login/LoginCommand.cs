using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using TokenHandler.Models;

namespace EcoFarm.UseCases.Accounts.Login
{
    public class LoginCommand : ICommand<LoginDTO>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string PrevUrl { get; set; }
    }

    public class LoginDTO
    {
        public string AccessToken { get; set; }
    }
    internal class LoginHandler : ICommandHandler<LoginCommand, LoginDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public LoginHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;

        }
        public async Task<Result<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var people = await _unitOfWork.Accounts
                    .GetQueryable()
                    .Where(x => x.USERNAME.Equals(request.UsernameOrEmail)
                    || x.EMAIL.Equals(request.UsernameOrEmail))
                    .FirstOrDefaultAsync();
            if (people is null)
            {
                return Result.Error("Tài khoản hoặc mật khẩu không đúng!");
            }
            var hashedPassword = HelperExtensions.HmacSha256ToHexString(request.Password, people.SALT);
            if (string.Compare(hashedPassword, people.HASHED_PASSWORD) != 0)
            {
                return Result.Error("Tài khoản hoặc mật khẩu không đúng!");
            }
            if (people.IS_ACTIVE == false)
            {
                return Result.Error($"Tài khoản của bạn đã bị khóa do {people.LOCKED_REASON}! Vui lòng liên hệ quản trị viên nếu có sự nhầm lẫn");
            }
            
            AccountTokenData account = new AccountTokenData
            {
                Fullname = people.NAME,
                Username = people.USERNAME,
                Email = people.EMAIL,
                ExpireDateTime = DateTime.Now.AddDays(3),
            };

            switch (people.ACCOUNT_TYPE)
            {
                case Domain.Common.Values.Enums.HelperEnums.AccountType.Seller: 
                    var erp = await _unitOfWork.SellerEnterprises
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(people.ID));
                    if (erp is null)
                    {
                        return Result.Error("Tài khoản không tồn tại hoặc đã bị xóa");
                    }
                    account.EntityId = erp.ID;
                    break;
                case Domain.Common.Values.Enums.HelperEnums.AccountType.Customer:
                    var user = await _unitOfWork.Users
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(people.ID));
                    if (user is null)
                    {
                        return Result.Error("Tài khoản không tồn tại hoặc đã bị xóa");
                    }
                    account.EntityId = user.ID;
                    break;
                case Domain.Common.Values.Enums.HelperEnums.AccountType.Admin:
                    account.EntityId = people.ID;
                    break;
                default:
                    return Result.CriticalError("Lỗi khi xử lý thông tin tài khoản");
            }
            var token = _authService.GenerateToken(account);
            people.LAST_LOGGED_IN = DateTime.Now;
            people.LATEST_GENERATED_TOKEN = token;
            people.IS_EMAIL_CONFIRMED = true;
            _unitOfWork.Accounts.Update(people);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new LoginDTO { AccessToken = token }, "Đăng nhập thành công");
        }
    }
}
