using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;

namespace EcoFarm.Domain.Entities;

[Table("SHOPPING_CART")]
public class ShoppingCart : BaseEntity
{
    public string USER_ID { get; set; }
    public bool IS_ORDERED { get; set; } = false;
    public int? TOTAL_QUANTITY { get; set; }
    public decimal? TOTAL_PRICE { get; set; }
    [InverseProperty(nameof(CartDetail.Cart))]
    public virtual ICollection<CartDetail> CartDetails { get; set; }
    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(User.ShoppingCarts))]
    public virtual User UserInfo { get; set; }
}