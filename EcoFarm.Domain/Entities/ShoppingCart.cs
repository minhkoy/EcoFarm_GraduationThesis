using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities;

[Table("SHOPPING_CART")]
public class ShoppingCart : BaseEntity
{
    public int? TRANSACTION_TYPE { get; set; }
}