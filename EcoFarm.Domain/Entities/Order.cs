using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Constants;
using EcoFarm.Domain.Entities.Administration;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities;

[Table("ORDER")]
public class Order : BaseEntity
{
    public string USER_ID { get; set; }
    public string ENTERPRISE_ID { get; set; }
    public string NOTE { get; set; }
    public int TOTAL_QUANTITY { get; set; }
    public decimal TOTAL_PRICE { get; set; }
    public decimal TOTAL_WEIGHT { get; set; }
    public string ADDRESS_ID { get; set; }
    public string ADDRESS_DESCRIPTION { get; set; }
    public OrderStatus STATUS { get; set; } = OrderStatus.WaitingSellerConfirm;
    public OrderPaymentMethod PAYMENT_METHOD { get; set; } = OrderPaymentMethod.Cash;
    [NotMapped]
    public string PaymentMethodName { get => EFX.PaymentMethods.dctPaymentMethod[PAYMENT_METHOD]; }
    //Inverse properties

    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(User.Orders))]
    public virtual User UserInfo { get; set; }
    [ForeignKey(nameof(ENTERPRISE_ID))]
    [InverseProperty(nameof(SellerEnterprise.Orders))]
    public virtual SellerEnterprise EnterpriseInfo { get; set; }
    [ForeignKey(nameof(ADDRESS_ID))]
    [InverseProperty(nameof(UserAddress.Orders))]
    public virtual UserAddress AddressInfo { get; set; }
    [InverseProperty(nameof(OrderProduct.OrderInfo))]
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    [InverseProperty(nameof(OrderTimeline.OrderInfo))]
    public virtual ICollection<OrderTimeline> OrderTimelines { get; set; }
}