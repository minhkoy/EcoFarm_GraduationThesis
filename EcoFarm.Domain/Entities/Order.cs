using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities;

[Table("ORDER")]
public class Order : BaseEntity
{
    [Column("PRODUCT_ID")] public string ProductId { get; set; }
    [Column("USER_ID")] public string UserId { get; set; }
    [Column("QUANTITY")] public int Quantity { get; set; }
}