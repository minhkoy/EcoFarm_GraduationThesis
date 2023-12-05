using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.FarmingPackages.Update
{
    public class UpdateFarmingPackageCommand : ICommand<FarmingPackageDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? EstimatedStartTime { get; set; }
        public DateTime? EstimatedEndTime { get; set; }
        public decimal? Price { get; set; }
        public int? QuantityRemain { get; set; }
        public bool? IsAutoCloseRegister { get; set; }
    }

    internal class UpdateFarmingPackageHandler : ICommandHandler<UpdateFarmingPackageCommand, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public UpdateFarmingPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<FarmingPackageDTO>> Handle(UpdateFarmingPackageCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            var account = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (account is null)
            {
                return Result<FarmingPackageDTO>.Unauthorized();
            }
            if (!account.IS_ACTIVE)
            {
                return Result.Error("Tài khoản bị khóa");
            }
            if (!account.IS_EMAIL_CONFIRMED)
            {
                return Result<FarmingPackageDTO>.Error("Tài khoản chưa được xác thực email");
            }
            var enterprise = await _unitOfWork.SellerEnterprises
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(account.ID));
            if (enterprise is null || !enterprise.IS_ACTIVE)
            {
                return Result<FarmingPackageDTO>.Forbidden();
            }
            var farmingPackage = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
            if (farmingPackage is null)
            {
                return Result<FarmingPackageDTO>.NotFound("Không tìm thấy thông tin gói farming");
            }
            if (!farmingPackage.IS_ACTIVE)
            {
                return Result<FarmingPackageDTO>.Error("Gói farming đã bị khóa");
            }
            if (!farmingPackage.SELLER_ENTERPRISE_ID.Equals(enterprise.ID))
            {
                return Result.Forbidden();
            }
            
            farmingPackage.CODE = request.Code;
            farmingPackage.NAME = request.Name;
            farmingPackage.DESCRIPTION = request.Description;
            farmingPackage.ESTIMATED_START_TIME = request.EstimatedStartTime;
            farmingPackage.ESTIMATED_END_TIME = request.EstimatedEndTime;
            farmingPackage.PRICE = request.Price ?? 0;
            farmingPackage.QUANTITY_START = request.QuantityRemain + farmingPackage.QUANTITY_REGISTERED;
            farmingPackage.IS_AUTO_CLOSE_REGISTER = request.IsAutoCloseRegister ?? false;

            _unitOfWork.FarmingPackages.Update(farmingPackage);
            await _unitOfWork.SaveChangesAsync();

            var registeredUser = _unitOfWork.UserRegisterPackages
                .GetQueryable()
                .Where(x => x.PACKAGE_ID.Equals(farmingPackage.ID));
            //.ToListAsync();
            return Result.Success(new FarmingPackageDTO
            {
                Id = farmingPackage.ID,
                Code = farmingPackage.CODE,
                Name = farmingPackage.NAME,
                Description = farmingPackage.DESCRIPTION,
                EstimatedStartTime = farmingPackage.ESTIMATED_START_TIME,
                EstimatedEndTime = farmingPackage.ESTIMATED_END_TIME,
                Price = farmingPackage.PRICE,
                QuantityStart = farmingPackage.QUANTITY_START,
                QuantityRegistered = farmingPackage.QUANTITY_REGISTERED,
                QuantityRemain = farmingPackage.QuantityRemain,
                PackageType = farmingPackage.PACKAGE_TYPE,
                SellerEnterpriseId = farmingPackage.SELLER_ENTERPRISE_ID,
                SellerEnterpriseName = enterprise.NAME,
                //SellerEnterpriseCode = enterprise.CODE,
                ServicePackageApprovalStatus = farmingPackage.STATUS,
                StartTime = farmingPackage.START_TIME,
                EndTime = farmingPackage.END_TIME,
                CloseRegisterTime = farmingPackage.CLOSE_REGISTER_TIME,
                RegisteredUsers = await registeredUser
                .Include(x => x.UserInfo)
                .Select(x => new FarmingPackageDTO.RegisteredUser
                {
                    AccountId = x.UserInfo.ACCOUNT_ID,
                    Name = x.UserInfo.NAME,
                })
                .ToListAsync()
            }, "Cập nhật thông tin gói thành công");
        }
    }
}
