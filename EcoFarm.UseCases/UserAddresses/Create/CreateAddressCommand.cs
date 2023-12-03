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

namespace EcoFarm.UseCases.UserAddresses.Create
{
    public class CreateAddressCommand : ICommand<UserAddressDTO>
    {
        public string AddressDescription { get; set; }
        public string ReceiverName { get; set; }
        public string AddressPhone { get; set; }
        public bool? IsMain { get; set; }
    }

    internal class CreateAddressHandler : ICommandHandler<CreateAddressCommand, UserAddressDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public CreateAddressHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<UserAddressDTO>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var accountType = _authService.GetAccountTypeName();
            if (string.Compare(accountType, EFX.AccountTypes.Customer) != 0)
            {
                return Result.Forbidden();
            }
            var userId = _authService.GetAccountEntityId();
            UserAddress userAddress = new UserAddress
            {
                USER_ID = userId,
                DESCRIPTION = request.AddressDescription,
                RECEIVER_NAME = request.ReceiverName,
                PHONE_NUMBER = request.AddressPhone,                
            };

            var mainAddress = await _unitOfWork.UserAddresses.GetQueryable()
                    .Where(x => x.USER_ID == userId && x.IS_MAIN == true)
                    .FirstOrDefaultAsync();
            if (mainAddress is not null)
            {
                if (request.IsMain.HasValue && request.IsMain.Value)
                {
                    mainAddress.IS_MAIN = false;
                    userAddress.IS_MAIN = true;
                    _unitOfWork.UserAddresses.Update(mainAddress);
                }
                else
                {
                    userAddress.IS_MAIN = false;
                }
            }
            else
            {
                userAddress.IS_MAIN = true;
            }

            _unitOfWork.UserAddresses.Add(userAddress);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success(new UserAddressDTO
            {
                Id = userAddress.ID,
                UserId = userAddress.USER_ID,
                AddressDescription = userAddress.DESCRIPTION,
                ReceiverName = userAddress.RECEIVER_NAME,
                AddressPhone = userAddress.PHONE_NUMBER,
                IsPrimary = userAddress.IS_MAIN,
            }, "Thêm mới địa chỉ thành công");
        }
    }
}
