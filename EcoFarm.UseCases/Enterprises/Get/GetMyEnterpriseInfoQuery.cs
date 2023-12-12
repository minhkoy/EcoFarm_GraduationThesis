using Ardalis.Result;
using EcoFarm.Application.Interfaces.Localization;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.Enterprises.Get
{
    public class GetMyEnterpriseInfoQuery : IQuerySingle<EnterpriseDTO>
    {
    }

    internal class Handler : IQuerySingleHandler<GetMyEnterpriseInfoQuery, EnterpriseDTO>
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
        public async Task<Result<EnterpriseDTO>> Handle(GetMyEnterpriseInfoQuery request, CancellationToken cancellationToken)
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
            var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(_authService.GetAccountEntityId());
            if (enterprise is null)
            {
                return Result.NotFound();
            }
            EnterpriseDTO enterpriseDTO = new()
            {
                AccountId = account.ID,
                Username = account.USERNAME,
                Email = account.EMAIL,
                AccountType = account.ACCOUNT_TYPE_NAME,
                AvatarUrl = account.AVATAR_URL,
                EnterpriseId = enterprise.ID,
                FullName = enterprise.NAME,
                Address = enterprise.ADDRESS,
                Hotline = enterprise.HOTLINE,
                Description = enterprise.DESCRIPTION,
                IsActive = enterprise.IS_ACTIVE,
                IsEmailConfirmed = account.IS_EMAIL_CONFIRMED,
                LockedReason = account.LOCKED_REASON,
                TaxCode = enterprise.TAX_CODE,
            };
            return Result<EnterpriseDTO>.Success(enterpriseDTO);
        }
    }
}
