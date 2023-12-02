using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities
{
    [Table("CART_DETAIL")]
    public class CartDetail : BaseNonExtendedEntity
    {
        public string PRODUCT_ID { get; set; }
        public string CART_ID { get; set; }
        public int? QUANTITY { get; set; }
        public decimal? PRICE { get; set; }
        public CurrencyType? CURRENCY { get; set; }
        
        [ForeignKey(nameof(CART_ID))]
        [InverseProperty(nameof(ShoppingCart.CartDetails))]
        public virtual ShoppingCart Cart { get; set; }
        [ForeignKey(nameof(PRODUCT_ID))]
        [InverseProperty(nameof(Product.CartDetails))]
        public virtual Product ProductInfo { get; set; }
    }
}
