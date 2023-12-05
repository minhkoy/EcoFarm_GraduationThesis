using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Accounts.ChangePassword
{
    public class ChangePasswordCommand : ICommand<bool>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    internal class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public ChangePasswordHandler(IUnitOfWork unitOfWork, IAuthService authService, ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null)
            {
                return Result<bool>.NotFound(_localizeService.GetMessage(LocalizationEnum.AccountNotExistedOrDeleted));
            }
            if (!account.IS_ACTIVE)
            {
                return Result.NotFound(_localizeService.GetMessage(LocalizationEnum.AccountLocked));
            }
            var hashedPassword = HelperExtensions.HmacSha256ToHexString(request.OldPassword, account.SALT);
            if (string.Compare(hashedPassword, account.HASHED_PASSWORD) != 0)
            {
                return Result<bool>.Error(_localizeService.GetMessage(LocalizationEnum.PasswordIncorrect));
            }
            
            account.SALT = HelperExtensions.GetRandomString(24);
            var newHashedPassword = HelperExtensions.HmacSha256ToHexString(request.NewPassword, account.SALT);
            account.HASHED_PASSWORD = newHashedPassword;
            _unitOfWork.Accounts.Update(account);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage(_localizeService.GetMessage(LocalizationEnum.ChangePasswordSuccessful));
        }
    }
}
