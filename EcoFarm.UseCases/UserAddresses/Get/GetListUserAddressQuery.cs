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

namespace EcoFarm.UseCases.UserAddresses.Get
{
    public class GetListUserAddressQuery : IQuery<UserAddressDTO>
    {
        public string UserId { get; set; }
        /// <summary>
        /// Keyword to search for user address description & receiver name
        /// </summary>
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class GetListUserAddressHander : IQueryHandler<GetListUserAddressQuery, UserAddressDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetListUserAddressHander(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<List<UserAddressDTO>>> Handle(GetListUserAddressQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.UserAddresses
                .GetQueryable();
            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(x => string.Equals(x.USER_ID, request.UserId));
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.DESCRIPTION.Contains(request.Keyword)
                    || x.RECEIVER_NAME.Contains(request.Keyword));
            }
            var result = await query
                .OrderByDescending(x => x.IS_MAIN)
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .Select(x => new UserAddressDTO
                {
                    Id = x.ID,
                    UserId = x.USER_ID,
                    AddressDescription = x.DESCRIPTION,
                    ReceiverName = x.RECEIVER_NAME,
                    AddressPhone = x.PHONE_NUMBER,
                    IsPrimary = x.IS_MAIN,
                    CreatedAt = x.CREATED_TIME,
                    ModifiedAt = x.MODIFIED_TIME,
                })
                .ToListAsync();
            return Result<List<UserAddressDTO>>.Success(result);
        }
    }
}
