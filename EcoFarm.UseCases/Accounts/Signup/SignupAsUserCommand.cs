using Ardalis.Result;
using EcoFarm.Application.Common.Extensions;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Accounts.Signup
{
    public class SignupAsUserCommand : ICommand<UserDTO>
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // TODO: Hash password
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public GenderEnum? Gender { get; set; }

    }

    internal class SignupAsUserHandler : ICommandHandler<SignupAsUserCommand, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SignupAsUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UserDTO>> Handle(SignupAsUserCommand request, CancellationToken cancellationToken)
        {
            var existedAccount = await _unitOfWork.Accounts
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.USERNAME.Equals(request.Username) || x.EMAIL.Equals(request.Email));
            if (existedAccount is not null)
            {
                if (existedAccount.EMAIL.Equals(request.Email))
                {
                    return Result.Error("Email đã tồn tại");
                }
                else
                {
                    return Result.Error("Tên đăng nhập đã tồn tại");
                }
            }
            string salt = HelperExtensions.GetRandomString(EFX.SaltLength);
            Account account = new()
            {
                NAME = request.Name,
                USERNAME = request.Username,
                EMAIL = request.Email,
                SALT = salt,
                HASHED_PASSWORD = HelperExtensions.HmacSha256ToHexString(request.Password, salt),
                ACCOUNT_TYPE = AccountType.Customer,
                LOCKED_REASON = string.Empty,
            };
            User user = new()
            {
                ACCOUNT_ID = account.ID,
                NAME = account.NAME,
                DATE_OF_BIRTH = request.DateOfBirth,
                PHONE_NUMBER = request.PhoneNumber,
                GENDER = request.Gender,
            };
            _unitOfWork.Accounts.Add(account);
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new UserDTO
            {
                AccountId = account.ID,
                FullName = account.NAME,
                Username = account.USERNAME,
                Email = account.EMAIL,
                IsEmailConfirmed = account.IS_EMAIL_CONFIRMED,
                DateOfBirth = user.DATE_OF_BIRTH,
                AccountType = account.ACCOUNT_TYPE_NAME,
                IsActive = account.IS_ACTIVE,
                LockedReason = string.Empty,
                AvatarUrl = account.AVATAR_URL,
                UserId = user.ID,
                Gender = user.GENDER,
                GenderName = user.GENDER.HasValue ? EFX.Genders.dctGenderEnum[user.GENDER.Value] : string.Empty,
                PhoneNumber = user.PHONE_NUMBER,
            });
        }
    }
}
