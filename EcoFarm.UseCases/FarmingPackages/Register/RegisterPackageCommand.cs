﻿using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingPackages.Register
{
    public class RegisterPackageCommand : ICommand<bool>
    {
        //public string UserId { get; set; }
        public string PackageId { get; set; }
        public RegisterPackageCommand(string packageId)
        {
            PackageId = packageId;
        }
        public RegisterPackageCommand()
        {

        }
    }

    internal class RegisterPackageHandler : ICommandHandler<RegisterPackageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public RegisterPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(RegisterPackageCommand request, CancellationToken cancellationToken)
        {
            if (!string.Equals(_authService.GetAccountTypeName(), AccountType.Customer.ToString()))
            {
                return Result.Forbidden();
            }
            var username = _authService.GetUsername();
            if (username is null)
            {
                return Result<bool>.Unauthorized();
            }
            var userAccount = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (userAccount is null)
            {
                return Result.Error("Tên đăng nhập không chính xác");
            }
            if (!userAccount.IS_ACTIVE)
            {
                return Result.Error("Tài khoản người dùng đang bị khóa");
            }
            if (!userAccount.IS_EMAIL_CONFIRMED)
            {
                return Result.Error("Tài khoản chưa được xác thực email. Vui lòng xác thực và thử lại");
            }

            var package = await _unitOfWork.FarmingPackages
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ID.Equals(request.PackageId));
            if (package is null)
            {
                return Result.Error("Thông tin gói farming không chính xác");
            }
            if (!package.IS_ACTIVE)
            {
                return Result.Error("Gói farming tạm thời bị khóa. Vui lòng thử lại sau");
            }
            if (package.CLOSE_REGISTER_TIME.HasValue)
            {
                return Result.Error("Rất tiếc, gói farming đã đóng đăng ký.");
            }
            var userId = _authService.GetAccountEntityId();
            var userRegisterPackage = await _unitOfWork.UserRegisterPackages
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USER_ID.Equals(userId) && x.PACKAGE_ID.Equals(package.ID));
            if (userRegisterPackage is not null)
            {
                return Result.Error("Bạn đã đăng ký gói farming này.");
            }

            
            _unitOfWork.UserRegisterPackages.Add(new UserRegisterPackage
            {
                USER_ID = userId,
                PACKAGE_ID = package.ID, 
                PRICE = package.PRICE,
                CURRENCY = package.CURRENCY,

            });

            package.QUANTITY_REGISTERED++;
            if (package.QuantityRemain <= 0)
            {
                package.CLOSE_REGISTER_TIME = DateTime.Now.ToVnDateTime();
            }
            _unitOfWork.FarmingPackages.Update(package);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Đăng ký gói farming thành công. Bạn sẽ nhận được thông tin từ các hoạt động mới nhất của gói farming này.");
            //if (package.STATUS i)
            //XXX
        }
    }
}
