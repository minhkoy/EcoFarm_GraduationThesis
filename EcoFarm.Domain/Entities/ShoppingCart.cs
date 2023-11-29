using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities;

[Table("SHOPPING_CART")]
public class ShoppingCart : BaseEntity
{
    public string USER_ID { get; set; }
    public bool IS_PURCHASED { get; set; } = false;
    public double? TOTAL_QUANTITY { get; set; }
    public double? TOTAL_PRICE { get; set; }
    [InverseProperty(nameof(CartDetail.Cart))]
    public virtual ICollection<CartDetail> CartDetails { get; set; }
}