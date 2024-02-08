using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities
{
    [Table("PRODUCT")]
    public class Product : BaseEntity
    {
        public string DESCRIPTION { get; set; }
        public string PACKAGE_ID { get; set; }
        public string ENTERPRISE_ID { get; set; }
        public int? TYPE { get; set; }
        /// <summary>
        /// Khối lượng sản phẩm (kg)
        /// </summary>
        public decimal WEIGHT { get; set; } = 0;
        public int? QUANTITY { get; set; } = 0;
        public int? SOLD { get; set; } = 0;
        public decimal? PRICE { get; set; }
        /// <summary>
        /// Price for user who registered the package
        /// </summary>
        public decimal? PRICE_FOR_REGISTERED { get; set; }
        public CurrencyType? CURRENCY { get; set; } = CurrencyType.VND;

        [NotMapped]
        public int? CURRENT_QUANTITY { get => (QUANTITY ?? 0) - (SOLD ?? 0); }

        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.ProductInfo))]
        public virtual FarmingPackage Package { get; set; }
        [ForeignKey(nameof(ENTERPRISE_ID))]
        [InverseProperty(nameof(SellerEnterprise.Products))]
        public virtual SellerEnterprise Enterprise { get; set; }
        [InverseProperty(nameof(ProductMedia.OwnedProduct))]
        public virtual ICollection<ProductMedia> ProductMedias { get; set; }
        [InverseProperty(nameof(CartDetail.ProductInfo))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        [InverseProperty(nameof(OrderProduct.ProductInfo))]
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
