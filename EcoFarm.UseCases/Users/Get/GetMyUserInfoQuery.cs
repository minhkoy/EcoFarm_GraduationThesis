using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Users.Get
{
    public class GetMyUserInfoQuery : IQuerySingle<UserDTO>
    {
    }

    internal class Handler : IQuerySingleHandler<GetMyUserInfoQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ILocalizeService _localizeService;
        public Handler(IUnitOfWork unitOfWork, IAuthService authService, ILocalizeService localizeService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _localizeService = localizeService;
        }
        public async Task<Result<UserDTO>> Handle(GetMyUserInfoQuery request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null)
            {
                return Result.NotFound();
            }
            if (!account.IS_ACTIVE)
            {
                return Result.Error(string.Format(_localizeService.GetMessage(Application.Localization.LocalizationEnum.AccountLocked), account.LOCKED_REASON));
            }
            var user = await _unitOfWork.Users.FindAsync(_authService.GetAccountEntityId());
            if (user is null)
            {
                return Result.NotFound();
            }
            UserDTO result = new()
            {
                AccountId = account.ID,
                Username = account.USERNAME,
                Email = account.EMAIL,
                PhoneNumber = user.PHONE_NUMBER,
                IsEmailConfirmed = account.IS_EMAIL_CONFIRMED,
                IsActive = account.IS_ACTIVE,
                AccountType = account.ACCOUNT_TYPE_NAME,
                AvatarUrl = account.AVATAR_URL,
                DateOfBirth = user.DATE_OF_BIRTH,
                FullName = user.NAME,
                Gender = user.GENDER,
                GenderName = user.GENDER.HasValue ? EFX.Genders.dctGenderEnum[user.GENDER.Value] : string.Empty,
                LockedReason = account.LOCKED_REASON,
                UserId = user.ID,
            };
            return Result.Success(result);
        }
    }
}
