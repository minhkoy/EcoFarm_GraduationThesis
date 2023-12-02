using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Application.Interfaces.Repositories;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingActivities.Get
{
    public class GetListFarmingActivityQuery : IQuery<ActivityDTO>
    {
        /// <summary>
        /// Keyword cho tên gói farming
        /// </summary>
        public string Keyword { get; set; }

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
