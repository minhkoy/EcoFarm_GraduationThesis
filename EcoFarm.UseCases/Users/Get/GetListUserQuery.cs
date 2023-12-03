using Ardalis.Result;
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

namespace EcoFarm.UseCases.Users.Get
{
    public class GetListUserQuery : IQuery<UserDTO>
    {
        /// <summary>
        /// Keyword for Users' Name
        /// </summary>
        public string Name { get; set; }
        public bool? IsEmailConfirmed { get; set; }
        public bool? IsActive { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }
    internal class GetListUserHandler : IQueryHandler<GetListUserQuery, UserDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<UserDTO>>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User> temp = _unitOfWork.Users
                .GetQueryable()
                .Include(x => x.AccountInfo);
            if (!string.IsNullOrEmpty(request.Name))
            {
                temp = temp.Where(x => x.NAME.Contains(request.Name));
            }
            if (request.IsEmailConfirmed.HasValue)
            {
                temp = temp.Where(x => x.AccountInfo.IS_EMAIL_CONFIRMED == request.IsEmailConfirmed.Value);
            }
            if (request.IsActive.HasValue)
            {
                temp = temp.Where(x => x.AccountInfo.IS_ACTIVE == request.IsActive.Value);
            }
            var result = await temp
                .Skip((request.Page - 1) * request.Limit)
                .Select(x => new UserDTO
                {
                    AccountId = x.ACCOUNT_ID,
                    FullName = x.NAME,
                    Email = x.AccountInfo.EMAIL,
                    AccountType = x.AccountInfo.ACCOUNT_TYPE_NAME,
                    AvatarUrl = x.AccountInfo.AVATAR_URL,
                    IsEmailConfirmed = x.AccountInfo.IS_EMAIL_CONFIRMED,
                    IsActive = x.AccountInfo.IS_ACTIVE,
                    DateOfBirth = x.DATE_OF_BIRTH,
                    Gender = x.GENDER,
                    GenderName = x.GENDER.HasValue ? EFX.Genders.dctGenderEnum[x.GENDER.Value] : string.Empty,
                    LockedReason = x.AccountInfo.LOCKED_REASON,
                    PhoneNumber = x.PHONE_NUMBER,
                    UserId = x.ID,
                    Username = x.AccountInfo.USERNAME,

                })
                .ToListAsync();
            return Result.Success(result);
        }
    }
}
