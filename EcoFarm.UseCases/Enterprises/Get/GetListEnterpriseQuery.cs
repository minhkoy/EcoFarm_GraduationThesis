using Ardalis.Result;
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

namespace EcoFarm.UseCases.Enterprises.Get
{
    public class GetListEnterpriseQuery : IQuery<EnterpriseDTO>
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        /// <summary>
        /// Keyword cho Name, TaxCode
        /// </summary>
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = EFX.DefaultPageSize;
    }
    
    internal class GetListEnterpriseQueryHandler : IQueryHandler<GetListEnterpriseQuery, EnterpriseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetListEnterpriseQueryHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<List<EnterpriseDTO>>> Handle(GetListEnterpriseQuery request, CancellationToken cancellationToken)
        {
            IQueryable<SellerEnterprise> query = _unitOfWork.SellerEnterprises.GetQueryable()
                .Include(x => x.AccountInfo);
            if (!string.IsNullOrEmpty(request.Id))
            {
                var erp = await query.FirstOrDefaultAsync(x => string.Equals(request.Id, x.ID));
                if (erp is null)
                {
                    return Result.Success(new List<EnterpriseDTO>());
                }
                return Result.Success(new List<EnterpriseDTO>
                {
                    new EnterpriseDTO
                    {
                        EnterpriseId = erp.ID,
                        FullName = erp.NAME,
                        AccountId = erp.ACCOUNT_ID,
                        Address = erp.ADDRESS,
                        Email = erp.AccountInfo.EMAIL,
                        Hotline = erp.HOTLINE,
                        AccountType = erp.AccountInfo.ACCOUNT_TYPE_NAME,
                        AvatarUrl = erp.AccountInfo.AVATAR_URL,
                        Description = erp.DESCRIPTION,
                        IsActive = erp.IS_ACTIVE,
                        IsEmailConfirmed = erp.AccountInfo.IS_EMAIL_CONFIRMED,
                        TaxCode = erp.TAX_CODE,
                        LockedReason = erp.AccountInfo.LOCKED_REASON,
                        Username = erp.AccountInfo.USERNAME,
                    }
                });
            }
            if (!string.IsNullOrEmpty(request.AccountId))
            {
                var erp = await query.FirstOrDefaultAsync(x => string.Equals(request.AccountId, x.ACCOUNT_ID));
                if (erp is null)
                {
                    return Result.Success(new List<EnterpriseDTO>());
                }
                return Result.Success(new List<EnterpriseDTO>
                {
                    new EnterpriseDTO
                    {
                        EnterpriseId = erp.ID,
                        FullName = erp.NAME,
                        AccountId = erp.ACCOUNT_ID,
                        Address = erp.ADDRESS,
                        Email = erp.AccountInfo.EMAIL,
                        Hotline = erp.HOTLINE,
                        AccountType = erp.AccountInfo.ACCOUNT_TYPE_NAME,
                        AvatarUrl = erp.AccountInfo.AVATAR_URL,
                        Description = erp.DESCRIPTION,
                        IsActive = erp.IS_ACTIVE,
                        IsEmailConfirmed = erp.AccountInfo.IS_EMAIL_CONFIRMED,
                        TaxCode = erp.TAX_CODE,
                        LockedReason = erp.AccountInfo.LOCKED_REASON,
                        Username = erp.AccountInfo.USERNAME,
                    }
                });
            }
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x => x.NAME.Contains(request.Keyword) || x.TAX_CODE.Contains(request.Keyword));
            }
            var result = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new EnterpriseDTO
                {
                    EnterpriseId = x.ID,
                    FullName = x.NAME,
                    AccountId = x.ACCOUNT_ID,
                    Address = x.ADDRESS,
                    Email = x.AccountInfo.EMAIL,
                    Hotline = x.HOTLINE,
                    AccountType = x.AccountInfo.ACCOUNT_TYPE_NAME,
                    AvatarUrl = x.AccountInfo.AVATAR_URL,
                    Description = x.DESCRIPTION,
                    IsActive = x.IS_ACTIVE,
                    IsEmailConfirmed = x.AccountInfo.IS_EMAIL_CONFIRMED,
                    TaxCode = x.TAX_CODE,
                    LockedReason = x.AccountInfo.LOCKED_REASON,
                    Username = x.AccountInfo.USERNAME,
                })
                .ToListAsync();
            return Result.Success(result);
        }
    }

}
