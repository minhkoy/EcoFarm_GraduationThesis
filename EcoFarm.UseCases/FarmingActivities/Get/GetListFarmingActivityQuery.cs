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

namespace EcoFarm.UseCases.FarmingActivities.Get
{
    public class GetListFarmingActivityQuery : IQuery<ActivityDTO>
    {
        //Lấy ra thông tin 1 gói farming theo id
        public string Id { get; set; }
        /// <summary>
        /// Nếu không dùng Id: Trường bắt buộc để xác định hoạt động thuộc gói farming nào
        /// </summary>
        public string PackageId { get; set; }
        /// <summary>
        /// Keyword cho mô tả và tiêu đề hoạt động
        /// </summary>
        public string Keyword { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int Page { get; set; } = 1;
        public int Limit { get; set; } = EFX.DefaultPageSize;
    }

    internal class Handler : IQueryHandler<GetListFarmingActivityQuery, ActivityDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<ActivityDTO>>> Handle(GetListFarmingActivityQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Id))
            {
                var activity = await _unitOfWork.FarmingPackageActivties
                    .GetQueryable()
                    .Include(x => x.ActivityMedias)
                    .FirstOrDefaultAsync(x => string.Equals(x.ID, request.Id));//.FindAsync(request.Id);
                if (activity is not null)
                {
                    var singleResult = new List<ActivityDTO>
                    {
                        new ActivityDTO
                        {
                            Id = activity.ID,
                            Code = activity.CODE,
                            Title = activity.NAME,
                            Description = activity.DESCRIPTION,
                            ShortDescription = activity.SHORT_DESCRIPTION,
                            //Medias = resultSingle.MEDIAS,
                            CreatedTime = activity.CREATED_TIME,
                            CreatedBy = activity.CREATED_BY,
                            Medias = activity.ActivityMedias.Select(x => new ActivityMediaDTO
                            {
                                ImageUrl = x.MEDIA_URL
                            }).ToList(),
                        }
                    };
                    return Result.Success(singleResult);
                }
                else
                {
                    return Result.Success();
                }
            }
            var temp = _unitOfWork.FarmingPackageActivties
                .GetQueryable()
                .Include(x => x.ActivityMedias)
                .Where(x => string.Equals(x.PACKAGE_ID, request.PackageId));
            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                temp = temp.Where(x => x.NAME.Contains(request.Keyword) || x.DESCRIPTION.Contains(request.Keyword));
            }
            if (request.FromDate.HasValue)
            {
                temp = temp.Where(x => x.CREATED_TIME >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                temp = temp.Where(x => x.CREATED_TIME <= request.ToDate.Value);
            }
            var result = await temp
                .OrderByDescending(x => x.CREATED_TIME)
                .Skip((request.Page - 1) * request.Limit)
                .Take(request.Limit)
                .Select(x => new ActivityDTO
                {
                    Id = x.ID,
                    Code = x.CODE,
                    Title = x.NAME,
                    Description = x.DESCRIPTION,
                    ShortDescription = x.SHORT_DESCRIPTION,
                    //Medias = x.MEDIAS,
                    Medias = x.ActivityMedias.Select(x => new ActivityMediaDTO
                    {
                        ImageUrl = x.MEDIA_URL
                    }).ToList(),
                    CreatedTime = x.CREATED_TIME,
                    CreatedBy = x.CREATED_BY,

                })
                .ToListAsync();
            return Result<List<ActivityDTO>>.Success(result);
        }
    }
    //internal class GetListFarmingActivityHandler : IQueryHandler<GetListFarmingActivityQuery, ActivityDTO>
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    public GetListFarmingActivityHandler(IUnitOfWork unitOfWork)
    //    {
    //        _unitOfWork = unitOfWork;
    //    }
    //    public Task<Result<List<ActivityDTO>>> Handle(GetListFarmingActivityQuery request, CancellationToken cancellationToken)
    //    {
    //        var temp = _unitOfWork.FarmingPackageActivties
    //            .GetQueryable();
    //        if (temp.)
    //    }
    //}
}
