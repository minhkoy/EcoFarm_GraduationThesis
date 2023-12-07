using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Accounts.Signup
{
    public class SignupAsEnterpriseCommand : ICommand<EnterpriseDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string Hotline { get; set; }
        public string Email { get; set; }
    }

    internal class SignupAsEnterpriseHandler : ICommandHandler<SignupAsEnterpriseCommand, EnterpriseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SignupAsEnterpriseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<EnterpriseDTO>> Handle(SignupAsEnterpriseCommand request, CancellationToken cancellationToken)
        {
            var randomKey = HelperExtensions.GetRandomString(EFX.SaltLength);
            Account newAccount = new()
            {
                USERNAME = request.Username,
                SALT = randomKey,
                HASHED_PASSWORD = HelperExtensions.HmacSha256ToHexString(request.Password, randomKey),
                EMAIL = request.Email,
                IS_ACTIVE = true,
                IS_EMAIL_CONFIRMED = true,
                IS_DELETE = false,
                ACCOUNT_TYPE = AccountType.Seller,
                LATEST_GENERATED_TOKEN = Guid.NewGuid().ToString(),
            };
            SellerEnterprise newEnterprise = new()
            {
                NAME = request.Name ?? string.Empty,
                ADDRESS = request.Address,
                TAX_CODE = request.TaxCode,
                DESCRIPTION = request.Description,
                AVATAR_URL = request.AvatarUrl,
                HOTLINE = request.Hotline,                
            };
            _unitOfWork.Accounts.Add(newAccount);
            _unitOfWork.SellerEnterprises.Add(newEnterprise);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new EnterpriseDTO
            {
                EnterpriseId = newEnterprise.ID,
                FullName = newEnterprise.NAME,
                Address = newEnterprise.ADDRESS,
                TaxCode = newEnterprise.TAX_CODE,
                Description = newEnterprise.DESCRIPTION,
                AvatarUrl = newEnterprise.AVATAR_URL,
                Hotline = newEnterprise.HOTLINE,
                Email = newAccount.EMAIL,
                AccountId = newAccount.ID,
                Username = newAccount.USERNAME,
            });
        }
    }
}
