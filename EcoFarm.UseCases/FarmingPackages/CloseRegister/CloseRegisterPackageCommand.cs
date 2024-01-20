using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.FarmingPackages.CloseRegister
{
    public class CloseRegisterPackageCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public CloseRegisterPackageCommand() { }
        public CloseRegisterPackageCommand(string id)
        {
            Id = id;
        }
    }

    internal class CloseRegisterPackageHandler : ICommandHandler<CloseRegisterPackageCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public CloseRegisterPackageHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(CloseRegisterPackageCommand request, CancellationToken cancellationToken)
        {
            //XXX: Validation not implemented. Just happy case.
            var package = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
            if (package == null)
                return Result<bool>.NotFound("Không tìm thấy gói farming");
            var username = _authService.GetUsername();
            var user = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(x.USERNAME, username));
            if (user is null)
            {
                return Result.Unauthorized();
            }
            if (!package.SELLER_ENTERPRISE_ID.Equals(_authService.GetAccountEntityId()))
            {
                return Result.Forbidden();
            }
            if (package.CLOSE_REGISTER_TIME.HasValue)
            {
                return Result<bool>.Error("Gói farming đã đóng đăng ký");
            }
            package.CLOSE_REGISTER_TIME = DateTime.Now.ToVnDateTime();
            _unitOfWork.FarmingPackages.Update(package);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Kết thúc đăng ký gói farming thành công");
        }
    }
}
