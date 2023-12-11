using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;

namespace EcoFarm.UseCases.FarmingPackages.Get
{
    public class GetSinglePackageQuery : IQuerySingle<FarmingPackageDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
    }

    internal class GetSinglePackageQueryHandler : IQuerySingleHandler<GetSinglePackageQuery, FarmingPackageDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        public GetSinglePackageQueryHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<Result<FarmingPackageDTO>> Handle(GetSinglePackageQuery request, CancellationToken cancellationToken)
        {
            FarmingPackage pkg = null;
            if (request is not null)
            {
                if (!string.IsNullOrEmpty(request.Id))
                {
                    pkg = await _unitOfWork.FarmingPackages.FindAsync(request.Id);
                    
                }
                else if (!string.IsNullOrEmpty(request.Code))
                {
                    pkg = await _unitOfWork.FarmingPackages.GetQueryable()
                        .FirstOrDefaultAsync(x => x.CODE.Equals(request.Code));
                }
            }
            if (pkg is null)
            {
                return Result.NotFound();
            }
            var enterprise = await _unitOfWork.SellerEnterprises.FindAsync(pkg.SELLER_ENTERPRISE_ID);
            IQueryable<FarmingPackageDTO.RegisteredUser> users = _unitOfWork.UserRegisterPackages
                .GetQueryable()
                .Include(x => x.UserInfo)
                .Where(x => string.Equals(x.PACKAGE_ID, pkg.ID))
                .Select(x => new FarmingPackageDTO.RegisteredUser
                {
                    UserId = x.USER_ID,
                    AccountId = x.UserInfo.ACCOUNT_ID,
                    FullName = x.UserInfo.NAME,
                    RegisteredTime = x.REGISTER_TIME
                });
            var isRegistered = false;
            if (users
                .AsEnumerable()
                .Any(x => string.Equals(x.UserId, _authService.GetAccountEntityId())))
            {
                isRegistered = true;
            }
            //Console.WriteLine(isRegistered);
            return Result<FarmingPackageDTO>.Success(new FarmingPackageDTO {
                Id = pkg.ID,
                Code = pkg.CODE,
                Name = pkg.NAME,
                Description = pkg.DESCRIPTION,
                EstimatedStartTime = pkg.ESTIMATED_START_TIME,
                EstimatedEndTime = pkg.ESTIMATED_END_TIME,
                Enterprise = new EnterpriseDTO
                {
                    EnterpriseId = enterprise.ID,
                    FullName = enterprise.NAME,
                },
                RegisteredUsers = users.ToList(),
                Price = pkg.PRICE,
                QuantityStart = pkg.QUANTITY_START,
                QuantityRegistered = pkg.QUANTITY_REGISTERED,
                QuantityRemain = pkg.QuantityRemain,
                CloseRegisterTime = pkg.CLOSE_REGISTER_TIME,
                PackageType = pkg.PACKAGE_TYPE,
                ServicePackageApprovalStatus = pkg.STATUS,
                NumbersOfRating = pkg.NUMBERS_OF_RATING,
                AverageRating = pkg.AverageRating,
                IsRegisteredByCurrentUser = isRegistered,
                
            });
        }
    }
}
