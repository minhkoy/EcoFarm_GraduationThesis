using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.UserAddresses.Delete
{
    public class DeleteAddressCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public DeleteAddressCommand(string id)
        {
            Id = id;
        }
        public DeleteAddressCommand()
        {

        }
    }

    internal class DeleteAddressHandler : ICommandHandler<DeleteAddressCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public DeleteAddressHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<bool>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
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
            _unitOfWork.UserAddresses.Remove(address);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithMessage("Đã xóa địa chỉ");
        }
    }
}
