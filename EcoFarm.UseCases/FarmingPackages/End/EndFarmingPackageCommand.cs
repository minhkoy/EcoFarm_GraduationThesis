using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
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

namespace EcoFarm.UseCases.FarmingPackages.End
{
    public class EndFarmingPackageCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public EndFarmingPackageCommand()
        {

        }
        public EndFarmingPackageCommand(string id)
        {
            Id = id;
        }

    }

    internal class EndFarmingPackageHandler : ICommandHandler<EndFarmingPackageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public EndFarmingPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(EndFarmingPackageCommand request, CancellationToken cancellationToken)
        {
            var farmingPackage = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
            if (farmingPackage is null)
            {
                return Result.NotFound("Không tìm thấy thông tin gói farming");
            }

            var username = _authService.GetUsername();
            var user = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (user is null)
            {
                return Result.Unauthorized();
            }
            if (!farmingPackage.SELLER_ENTERPRISE_ID.Equals(_authService.GetAccountEntityId()))
            {
                return Result.Forbidden();
            }

            if (!farmingPackage.START_TIME.HasValue)
            {
                return Result.Error("Gói farming chưa bắt đầu");
            }
            if (farmingPackage.END_TIME.HasValue)
            {
                return Result.Error("Gói farming đã kết thúc");
            }

            farmingPackage.END_TIME = DateTime.Now.ToVnDateTime();
            _unitOfWork.FarmingPackages.Update(farmingPackage);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage($"Kết thúc gói farming thành công vào {farmingPackage.END_TIME}");
        }
    }
}
