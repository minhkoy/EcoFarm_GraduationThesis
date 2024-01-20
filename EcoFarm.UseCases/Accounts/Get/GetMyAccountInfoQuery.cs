using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Application.Localization;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Accounts.Get
{
    public class GetMyAccountInfoQuery : IQuerySingle<AccountDTO>
    {

    }

    internal class GetMyAccountInfoHandler : IQuerySingleHandler<GetMyAccountInfoQuery, AccountDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public GetMyAccountInfoHandler(IUnitOfWork unitOfWork, IAuthService authService,
            ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<AccountDTO>> Handle(GetMyAccountInfoQuery request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var userAccount = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (userAccount is null)
            {
                return Result.Error(_localizeService.GetMessage(LocalizationEnum.AccountNotExistedOrDeleted));
            }
            if (!userAccount.IS_ACTIVE)
            {
                return Result.Error(_localizeService.GetMessage(LocalizationEnum.AccountLocked));
            }
            AccountDTO accountDTO = new()
            {
                AccountId = userAccount.ID,
                Username = userAccount.USERNAME,
                Email = userAccount.EMAIL,
                AccountType = userAccount.ACCOUNT_TYPE_NAME,
                IsActive = userAccount.IS_ACTIVE,
                IsEmailConfirmed = userAccount.IS_EMAIL_CONFIRMED,
                AvatarUrl = userAccount.AVATAR_URL,
                FullName = userAccount.NAME,
                LockedReason = userAccount.LOCKED_REASON,
                AccountEntityId = _authService.GetAccountEntityId(),

            };
            return Result<AccountDTO>.Success(accountDTO);
        }
    }
}
