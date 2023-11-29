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
        public int? TYPE { get; set; }
        public int? QUANTITY { get; set; }
        public int? SOLD { get; set; }
        public decimal? PRICE { get; set; }
        /// <summary>
        /// Price for user who registered the package
        /// </summary>
        public decimal? PRICE_FOR_REGISTERED { get; set; }
        public CurrencyType? CURRENCY { get; set; }

        [NotMapped]
        public int? CURRENT_QUANTITY { get => QUANTITY - SOLD; }

        [ForeignKey(nameof(PACKAGE_ID))]
        [InverseProperty(nameof(FarmingPackage.Products))]
        public virtual FarmingPackage Package { get; set; }
        [InverseProperty(nameof(ProductMedia.OwnedProduct))]
        public virtual ICollection<ProductMedia> ProductMedias { get; set; }
        [InverseProperty(nameof(CartDetail.ProductInfo))]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
