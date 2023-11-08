using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    [Table("CART_DETAIL")]
    public class CartDetail : BaseNonExtendedEntity
    {
        public string SERVICE_ID { get; set; }
        public string CART_ID { get; set; }
        public int? QUANTITY { get; set; }
        public decimal? PRICE { get; set; }

        [ForeignKey(nameof(CART_ID))]
        [InverseProperty(nameof(ShoppingCart.CartDetails))]
        public virtual ShoppingCart Cart { get; set; }
        [ForeignKey(nameof(SERVICE_ID))]
        [InverseProperty(nameof(ServicePackage.CartDetails))]
        public virtual ServicePackage Service { get; set; }
    }
}
