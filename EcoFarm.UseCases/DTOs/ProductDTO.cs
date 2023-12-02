using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Weight { get; set; }
        public string PackageId { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public int? Quantity { get; set; }
        public int? Sold { get; set; }
        public int? QuantityRemain { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceForRegistered { get; set; }
        public CurrencyType? Currency { get; set; }
        public DateTime CreatedTime { get; set; }
        public string SellerEnterpriseId { get; set; }
        public string SellerEnterpriseName { get; set; }
    }
}
