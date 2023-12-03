using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.UserAddresses.Update
{
    public class UpdateAddressCommand : ICommand<UserAddressDTO>
    {
        public string Id { get; set; }
        public string AddressDescription { get; set; }
        public string ReceiverName { get; set; }
        public string AddressPhone { get; set; }
    }

    internal class UpdateAddressHandler : ICommandHandler<UpdateAddressCommand, UserAddressDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public UpdateAddressHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<UserAddressDTO>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
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
            if (address.USER_ID != _authService.GetAccountEntityId())
            {
                return Result.Forbidden();
            }
            address.DESCRIPTION = request.AddressDescription;
            address.RECEIVER_NAME = request.ReceiverName;
            address.PHONE_NUMBER = request.AddressPhone;

            _unitOfWork.UserAddresses.Update(address);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new UserAddressDTO
            {
                Id = address.ID,
                UserId = address.USER_ID,
                AddressDescription = address.DESCRIPTION,
                ReceiverName = address.RECEIVER_NAME,
                AddressPhone = address.PHONE_NUMBER,
                IsPrimary = address.IS_MAIN,
            }, "Cập nhật địa chỉ thành công");
        }
    }
}
