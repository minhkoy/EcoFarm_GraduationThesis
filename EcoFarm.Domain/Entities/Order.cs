using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities;

[Table("ORDER")]
public class Order : BaseEntity
{
    [Column("PACKAGE_ID")] public string PackageId { get; set; }
    [Column("USER_ID")] public string UserId { get; set; }
    [Column("QUANTITY")] public int Quantity { get; set; }
    [Column("STATUS")] public OrderStatus Status { get; set; } = OrderStatus.WaitingSellerConfirm;
    //Inverse properties
    [ForeignKey(nameof(PackageId))]
    [InverseProperty(nameof(ServicePackage.Orders))]
    public virtual ServicePackage Package { get; set; }

    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(User.Orders))]
    public virtual User UserOrdered { get; set; }
}