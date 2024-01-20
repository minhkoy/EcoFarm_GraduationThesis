using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Infrastructure.Services.Interfaces;
using EcoFarm.UseCases.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingActivities.Edit
{
    public class UpdateActivityCommand : ICommand<ActivityDTO>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string MainImage { get; set; }
        public List<string> Medias { get; set; }
        public List<string> Images { get; set; }
        public string Title { get; set; }
    }

    internal class Handler : ICommandHandler<UpdateActivityCommand, ActivityDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAuthService _authService;
        public Handler(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
            _authService = authService;
        }
        public async Task<Result<ActivityDTO>> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            if (_authService.GetAccountTypeName() != EFX.AccountTypes.Seller)
            {
                return Result.Forbidden();
            }
            var erpId = _authService.GetAccountEntityId();
            var erp = await _unitOfWork.SellerEnterprises.FindAsync(erpId);
            if (erp == null)
            {
                return Result.Forbidden();
            }
            var activity = await _unitOfWork.FarmingPackageActivties.FindAsync(request.Id);
            if (activity == null)
            {
                return Result.NotFound("Không tìm thấy thông tin hoạt động farming");
            }
            var pkg = await _unitOfWork.FarmingPackages.FindAsync(activity.PACKAGE_ID);
            if (pkg == null)
            {
                return Result.NotFound("Không tìm thấy thông tin gói farming cần thay đổi hoạt động");
            }
            if (!pkg.IS_ACTIVE)
            {
                return Result.Error("Gói farming đã bị khóa");
            }
            if (pkg.STATUS != ServicePackageApprovalStatus.Approved)
            {
                return Result.Error("Gói farming chưa được duyệt");
            }
            if (pkg.SELLER_ENTERPRISE_ID != erpId)
            {
                return Result.Forbidden();
            }
            if (!pkg.START_TIME.HasValue)
            {
                return Result.Error("Gói farming chưa bắt đầu");
            }
            if (pkg.END_TIME.HasValue)
            {
                return Result.Error("Gói farming đã kết thúc");
            }
            #region Update 
            activity.CODE = request.Code;
            activity.NAME = request.Title;
            activity.SHORT_DESCRIPTION = request.ShortDescription;
            activity.DESCRIPTION = request.Content;
            _unitOfWork.FarmingPackageActivties.Update(activity);
            var medias = _unitOfWork.ActivityMedias
                .GetQueryable()
                .Where(x => string.Equals(x.ACTIVITY_ID, activity.ID));
                //.ToListAsync();
            _unitOfWork.ActivityMedias.RemoveRange(medias);
            var newImageUrl = _cloudinaryService.UploadBase64Image(request.MainImage);
            if (!string.IsNullOrEmpty(newImageUrl))
            {
                _unitOfWork.ActivityMedias.Add(new Domain.Entities.ActivityMedia
                {
                    ACTIVITY_ID = activity.ID,
                    MEDIA_URL = newImageUrl,
                });
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(new ActivityDTO
            {
                Id = activity.ID,
                Code = activity.CODE,
                Title = activity.NAME,
                ShortDescription = activity.SHORT_DESCRIPTION,
                Description = activity.DESCRIPTION,
                MediaUrls = newImageUrl,
                CreatedTime = activity.CREATED_TIME,
                
            });

            #endregion
        }
    }
}
