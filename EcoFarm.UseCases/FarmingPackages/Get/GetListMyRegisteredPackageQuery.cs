using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingPackages.Get
{
    public class GetListMyRegisteredPackageQuery : IQuery<FarmingPackageDTO>
    {
        public string Keyword { get; set; }
        public string EnterpriseId { get; set; }
        public bool IsAcive { get; set; } = true;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = EFX.DefaultPageSize;

    }
}
