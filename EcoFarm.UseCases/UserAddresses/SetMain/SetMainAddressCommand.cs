using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.UserAddresses.SetMain
{
    public class SetMainAddressCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public SetMainAddressCommand(string id)
        {
            Id = id;
        }
        public SetMainAddressCommand()
        {

        }
    }

    internal class SetMainAddressHandler : ICommandHandler<SetMainAddressCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public SetMainAddressHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result<bool>> Handle(SetMainAddressCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (accountType != EFX.AccountTypes.Customer)
            {
                return Result.Forbidden();
            }
            var address = await _unitOfWork.UserAddresses.FindAsync(request.Id);
            if (address is null)
            {
                return Result.NotFound("Không tìm thấy thông tin địa chỉ");
            }
            var userId = _authService.GetAccountEntityId();
            if (!string.Equals(userId, address.USER_ID))
            {
                return Result.Forbidden();
            }
            var currentMainAddress = await _unitOfWork.UserAddresses
                .GetQueryable()
                .FirstOrDefaultAsync(x => string.Equals(userId, x.USER_ID) && x.IS_MAIN == true);
            if (currentMainAddress is not null)
            {
                currentMainAddress.IS_MAIN = false;
                _unitOfWork.UserAddresses.Update(currentMainAddress);
            }
            address.IS_MAIN = true;
            _unitOfWork.UserAddresses.Update(address);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Đã thay đổi địa chỉ mặc định");
        }
    }
}
