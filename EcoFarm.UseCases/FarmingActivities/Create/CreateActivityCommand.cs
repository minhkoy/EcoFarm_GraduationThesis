using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.Domain.Entities;
using EcoFarm.Infrastructure.Services.Interfaces;
using EcoFarm.UseCases.Common.BackgroundJobs.BatchJobs;
using EcoFarm.UseCases.DTOs;
using Hangfire;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenHandler.Interfaces;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.FarmingActivities.Create
{
    public class CreateActivityCommand : ICommand<ActivityDTO>
    {
        public string Code { get; set; }
        public string PackageId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string MainImage { get; set; }
        public List<string> Medias { get; set; }
        public List<IFormFile> Images { get; set; }

        //public 
    }

    internal class CreateActivityHandler : ICommandHandler<CreateActivityCommand, ActivityDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly ICloudinaryService _cloudinaryService;
        public CreateActivityHandler(IUnitOfWork unitOfWork, IAuthService authService,
            ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<Result<ActivityDTO>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var erpId = _authService.GetAccountEntityId();
            var erp = await _unitOfWork.SellerEnterprises.FindAsync(erpId);
            if (erp == null)
            {
                return Result.Forbidden();
            }
            var pkg = await _unitOfWork.FarmingPackages.FindAsync(request.PackageId);
            if (pkg == null)
            {
                return Result.NotFound("Không tìm thấy thông tin gói farming");
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
            var activity = new FarmingPackageActivity
            {
                CODE = request.Code,
                NAME = request.Title,
                SHORT_DESCRIPTION = request.ShortDescription,
                DESCRIPTION = request.Content,
                PACKAGE_ID = request.PackageId,
                
            };
            _unitOfWork.FarmingPackageActivties.Add(activity);
            //request.Images.ForEach(image =>
            //{
            //    var rs = _cloudinaryService.UploadImageFromFile(image);
            //    if (!string.IsNullOrEmpty(rs))
            //    {
            //        //BackgroundJob.Enqueue();
            //        BackgroundJob.Enqueue<SaveActivityImages>(x => x.Run(activity.ID, request.Images));
            //    }
            //});
            var avatarUrl = _cloudinaryService.UploadBase64Image(request.MainImage);
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                _unitOfWork.ActivityMedias.Add(new()
                {
                    ACTIVITY_ID = activity.ID,
                    MEDIA_URL = avatarUrl,
                });
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Success(new ActivityDTO
            {
                Id = activity.ID,
                Code = activity.CODE,
                Title = activity.NAME,
                ShortDescription = activity.SHORT_DESCRIPTION,
                Description = activity.DESCRIPTION,
                PackageId = activity.PACKAGE_ID,
                PackageCode = pkg.CODE,
                PackageName = pkg.NAME,
                CreatedTime = activity.CREATED_TIME,
                CreatedBy = activity.CREATED_BY,
                MediaUrls = avatarUrl,


            }, "Thêm mới hoạt động thành công");
        }
    }
}
