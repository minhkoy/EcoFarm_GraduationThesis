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
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Accounts.Update
{
    public class UpdateUserCommand : ICommand<UserDTO>
    {
        public string Fullname { get; set; }
        /// <summary>
        /// Base64 avatar image
        /// </summary>
        public string Avatar { get; set; }
        public GenderEnum? Gender { get; set; }
        public string PhoneNumber { get; set; }
    }

    internal class UpdateUserHandler : ICommandHandler<UpdateUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public UpdateUserHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var username = _authService.GetUsername();
            if (username is null)
            {
                return Result.Unauthorized();
            }
            var userAccount = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(username));
            if (userAccount.ACCOUNT_TYPE != AccountType.Customer)
            {
                return Result.Forbidden();
            }
            var user = await _unitOfWork.Users
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.ACCOUNT_ID.Equals(userAccount.ID));

            byte[] avatarBytes = Convert.FromBase64String(request.Avatar);
            using (MemoryStream ms = new(avatarBytes))
            {

            }
            userAccount.NAME = request.Fullname;
            user.GENDER = request.Gender;
            user.PHONE_NUMBER = request.PhoneNumber;
            //user.AVATAR = request.Avatar;
            _unitOfWork.Users.Update(user);
            _unitOfWork.Accounts.Update(userAccount);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new UserDTO
            {
                AccountId = user.ACCOUNT_ID,
                FullName = userAccount.NAME,
                AccountType = userAccount.ACCOUNT_TYPE_NAME,
                DateOfBirth = user.DATE_OF_BIRTH,
                IsEmailConfirmed = userAccount.IS_EMAIL_CONFIRMED,
            }, "Cập nhật thông tin thành công");
        }
    }
}
