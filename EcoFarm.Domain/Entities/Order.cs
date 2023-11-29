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
    public string NOTE { get; set; }
    public int QUANTITY { get; set; }
    public OrderStatus STATUS { get; set; } = OrderStatus.WaitingSellerConfirm;
    public ServiceType SERVICE_TYPE { get; set; }
    //Inverse properties
    [ForeignKey(nameof(PACKAGE_ID))]
    [InverseProperty(nameof(FarmingPackage.Orders))]
    public virtual FarmingPackage Package { get; set; }

    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(User.Orders))]
    public virtual User UserInfo { get; set; }
}