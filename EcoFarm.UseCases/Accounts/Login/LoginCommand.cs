using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.Domain.Common.Values.Enums;
using EcoFarm.UseCases.Common.Hubs;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
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
        public bool? IsRemember { get; set; }
    }

    public class LoginDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string AccountType { get; set; }
    }
    internal class LoginHandler : ICommandHandler<LoginCommand, LoginDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public LoginHandler(IUnitOfWork unitOfWork, IAuthService authService, ILocalizeService localizeService,
            IHttpContextAccessor httpContextAccessor,
            IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _localizeService = localizeService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _notificationHub = notificationHub;

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
                return Result.Error(_localizeService.GetMessage(Application.Localization.LocalizationEnum.UsernameOrPasswordIncorrect));
            }
            var hashedPassword = HelperExtensions.HmacSha256ToHexString(request.Password, people.SALT);
            if (string.Compare(hashedPassword, people.HASHED_PASSWORD) != 0)
            {
                return Result.Error(_localizeService.GetMessage(Application.Localization.LocalizationEnum.UsernameOrPasswordIncorrect));
            }
            if (people.IS_ACTIVE == false)
            {
                return Result.Error(string.Format(_localizeService.GetMessage(LocalizationEnum.AccountLocked), people.LOCKED_REASON));
            }
            
            AccountTokenData account = new AccountTokenData
            {
                Fullname = people.NAME,
                Username = people.USERNAME,
                Email = people.EMAIL,
                ExpireDateTime = DateTime.Now.AddDays(3),
                AccountTypeName = people.ACCOUNT_TYPE_NAME,
            };

            switch (people.ACCOUNT_TYPE)
            {
                case HelperEnums.AccountType.Seller: 
                    var erp = await _unitOfWork.SellerEnterprises
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(people.ID));
                    if (erp is null)
                    {
                        return Result.Error(_localizeService.GetMessage(LocalizationEnum.AccountNotExistedOrDeleted));
                    }
                    account.EntityId = erp.ID;
                    break;
                case HelperEnums.AccountType.Customer:
                    var user = await _unitOfWork.Users
                        .GetQueryable()
                        .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(people.ID));
                    if (user is null)
                    {
                        return Result.Error(_localizeService.GetMessage(LocalizationEnum.AccountNotExistedOrDeleted));
                    }
                    account.EntityId = user.ID;
                    break;
                case HelperEnums.AccountType.Admin:
                    account.EntityId = people.ID;
                    break;
                default:
                    return Result.CriticalError("Lỗi khi xử lý thông tin tài khoản");
            }
            var token = _authService.GenerateToken(account, request.IsRemember);
            people.LAST_LOGGED_IN = DateTime.Now.ToVnDateTime();
            people.LATEST_GENERATED_TOKEN = token;
            people.IS_EMAIL_CONFIRMED = true;
            _unitOfWork.Accounts.Update(people);
            await _unitOfWork.SaveChangesAsync();
            await _notificationHub.Clients.All.SendAsync("ReceiveNotification", "Đăng nhập thành công");
            //BackgroundJob.Schedule(() => Task.Run(Console.WriteLine("XXXY")) , DateTime.Now) ;
            return Result.Success(new LoginDTO { AccessToken = token, AccountType = people.ACCOUNT_TYPE.ToString() }, _localizeService.GetMessage(Application.Localization.LocalizationEnum.LoginSuccessful));
        }
    }
}
