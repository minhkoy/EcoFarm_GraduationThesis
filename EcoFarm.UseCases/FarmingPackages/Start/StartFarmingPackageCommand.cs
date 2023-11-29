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

namespace EcoFarm.UseCases.FarmingPackages.Start
{
    public class StartFarmingPackageCommand : ICommand<FarmingPackageDTO>
    {
        public string FarmingPackageId { get; set; }
        public StartFarmingPackageCommand() { }
        public StartFarmingPackageCommand(string id)
        {
            FarmingPackageId = id;
        }

    }

    public class StartFarmingPackageHandler : ICommandHandler<StartFarmingPackageCommand, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public StartFarmingPackageHandler(IUnitOfWork unitOfWork,
            IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<FarmingPackageDTO>> Handle(StartFarmingPackageCommand request, CancellationToken cancellationToken)
        {
            var farmingPackage = await _unitOfWork.FarmingPackages.FindAsync(request.FarmingPackageId);
            if (farmingPackage is null)
            {
                return Result<FarmingPackageDTO>.NotFound("Không tìm thấy thông tin gói farming");
            }

            var username = _authService.GetUsername();
            var user = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(username));
            if (user is null)
            {
                return Result.Unauthorized();
            }
            if (!farmingPackage.SELLER_ENTERPRISE_ID.Equals(user.ID))
            {
                return Result.Forbidden();
            }

            if (farmingPackage.START_TIME.HasValue)
            {
                return Result<FarmingPackageDTO>.Error("Gói farming đã bắt đầu");
            }

            farmingPackage.START_TIME = DateTime.Now;
            _unitOfWork.FarmingPackages.Update(farmingPackage);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage($"Bắt đầu gói farming thành công vào lúc {farmingPackage.START_TIME}");
        }
    }
}
