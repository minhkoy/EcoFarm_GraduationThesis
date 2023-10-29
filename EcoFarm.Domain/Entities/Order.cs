using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities;

[Table("ORDER")]
public class Order : BaseEntity
{
    public string PACKAGE_ID { get; set; }
    public string USER_ID { get; set; }
    public int QUANTITY { get; set; }
    public OrderStatus STATUS { get; set; } = OrderStatus.WaitingSellerConfirm;
    //Inverse properties
    [ForeignKey(nameof(PACKAGE_ID))]
    [InverseProperty(nameof(ServicePackage.Orders))]
    public virtual ServicePackage Package { get; set; }

    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(User.Orders))]
    public virtual User UserOrdered { get; set; }
}