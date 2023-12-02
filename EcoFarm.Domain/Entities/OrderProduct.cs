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
    [Table("ORDER_PRODUCT")]
    public class OrderProduct : BaseNonExtendedEntity
    {
        public string ORDER_ID { get; set; }
        public string PRODUCT_ID { get; set; }
        public int QUANTITY { get; set; }
        public decimal? PRICE { get; set; }
        public decimal? WEIGHT { get; set; }
        public CurrencyType? CURRENCY { get; set; }
        [ForeignKey(nameof(ORDER_ID))]
        [InverseProperty(nameof(Order.OrderProducts))]
        public virtual Order OrderInfo { get; set; }
        [ForeignKey(nameof(PRODUCT_ID))]
        [InverseProperty(nameof(Product.OrderProducts))]
        public virtual Product ProductInfo { get; set; }
    }
}
