﻿using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Infrastructure.Services.Interfaces;
using EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs;
using EcoFarm.UseCases.DTOs;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingPackages.Create
{
    public class CreateFarmingPackageCommand : ICommand<FarmingPackageDTO>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartTime { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Avatar { get; set; }
        public FarmingPackageType? ServiceType { get; set; }
        public bool? IsAutoCloseRegister { get; set; }
    }

    internal class CreateFarmingPackageHandler : ICommandHandler<CreateFarmingPackageCommand, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ICloudinaryService _cloudinaryService;
        public CreateFarmingPackageHandler(IUnitOfWork unitOfWork, IAuthService authService,
            ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<Result<FarmingPackageDTO>> Handle(CreateFarmingPackageCommand request, CancellationToken cancellationToken)
        {
            #region BusinessValidate
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null)// || !account.LATEST_GENERATED_TOKEN.Equals(_authService.Token) )
            {
                return Result.Unauthorized();
            }
            if (!account.IS_ACTIVE || !account.IS_EMAIL_CONFIRMED)
            {
                return Result.Forbidden();
            }
            var enterprise = await _unitOfWork.SellerEnterprises
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(account.ID));
            if (enterprise is null || !enterprise.IS_ACTIVE)
            {
                return Result.Forbidden();
            }
            if (!enterprise.IS_APPROVED.HasValue || !enterprise.IS_APPROVED.Value)
            {
                return Result
                    .Error("Tài khoản nhà cung cấp chưa được duyệt nên chưa thể tạo mới gói farming. Vui lòng thử lại sau hoặc liên hệ hotline để biết thêm thông tin chi tiết.");
            }
            #endregion
            #region Create
            var avatarUrl = _cloudinaryService.UploadBase64Image(request.Avatar);

            var farmingPackage = new FarmingPackage
            {
                CODE = request.Code,
                NAME = request.Name,
                DESCRIPTION = request.Description,
                ESTIMATED_START_TIME = request.EstimatedStartTime,
                ESTIMATED_END_TIME = request.EstimatedEndTime,
                PRICE = request.Price ?? 0,
                QUANTITY_START = request.Quantity,
                PACKAGE_TYPE = request.ServiceType.Value,
                SELLER_ENTERPRISE_ID = enterprise.ID,
                IS_AUTO_CLOSE_REGISTER = request.IsAutoCloseRegister ?? false,
                AVATAR_URL = avatarUrl
            };
            _unitOfWork.FarmingPackages.Add(farmingPackage);
            await _unitOfWork.SaveChangesAsync();
            if (request.IsAutoCloseRegister.HasValue && request.IsAutoCloseRegister.Value && request.EstimatedStartTime.HasValue)
            {
                BackgroundJob.Schedule<AutoCloseRegisterPackageJob>(x => x.Run(farmingPackage.ID, request.EstimatedStartTime.Value), request.EstimatedStartTime.Value);
            }
            return Result.Success(new FarmingPackageDTO
            {
                Id = farmingPackage.ID,
                Code = farmingPackage.CODE,
                Name = farmingPackage.NAME,
                Description = farmingPackage.DESCRIPTION,
                EstimatedStartTime = farmingPackage.ESTIMATED_START_TIME,
                EstimatedEndTime = farmingPackage.ESTIMATED_END_TIME,
                Price = farmingPackage.PRICE,
                Currency = farmingPackage.CURRENCY,
                QuantityStart = farmingPackage.QUANTITY_START,
                QuantityRegistered = farmingPackage.QUANTITY_REGISTERED,
                QuantityRemain = farmingPackage.QuantityRemain,
                PackageType = farmingPackage.PACKAGE_TYPE,
                Enterprise = new()
                {
                    EnterpriseId = enterprise.ID,
                    FullName = enterprise.NAME,
                    Username = account.USERNAME,
                },
                
                //SellerEnterpriseCode = enterprise.CODE,
                ServicePackageApprovalStatus = farmingPackage.STATUS,
                CreatedTime = farmingPackage.CREATED_TIME,
                CreatedBy = farmingPackage.CREATED_BY,
                NumbersOfRating = farmingPackage.NUMBERS_OF_RATING,
                AverageRating = farmingPackage.AverageRating,


            }, "Thêm mới gói farming thành công");
            #endregion
        }
    }
}
