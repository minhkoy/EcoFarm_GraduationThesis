using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.DTOs.Stats
{
    public class EnterpriseStatDTO
    {
        public string EnterpriseId { get; set; }
        public string EnterpriseName { get; set; }
        public long TotalRegisterPackageInTimeRange { get; set; }
        public long TotalSoldProductInTimeRange { get; set; }
        public decimal TotalOrderPriceInTimeRange { get; set; }
        public decimal TotalOrderPriceSoFar { get; set; }
        public List<ProductDTO> TopSoldProductsInTimeRange { get; set; }
        public List<FarmingPackageDTO> TopRegisteredPackagesInTimeRange { get; set; }
    }
}
