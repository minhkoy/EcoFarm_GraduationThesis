using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Enums;

namespace EcoFarm.Domain.Entities;

[Table("SERVICE_PACKAGE")]
public class ServicePackage : BaseEntity
{
    public string DESCRIPTION { get; set; }
    public string SELLER_ENTERPRISE_ID { get; set; }
    public DateTime START_TIME { get; set; }
    public DateTime? END_TIME { get; set; }
    public int? QUANTITY_START { get; set; }
    public int QUANTITY_SOLD { get; set; }
    public int SERVICE_TYPE { get; set; }
    public double PRICE { get; set; } = 0;
    public int STATUS { get; set; } = (int)HelperEnums.ServicePackageApprovalStatus.Pending;

    //Extension properties
    [NotMapped] public int QuantityRemain => !QUANTITY_START.HasValue ? 0 : QUANTITY_START.Value - QUANTITY_SOLD;
    //Inverse properties
    [ForeignKey(nameof(SELLER_ENTERPRISE_ID))]
    [InverseProperty(nameof(SellerEnterprise.EnterpiseServicePackages))]
    public virtual SellerEnterprise Enterprise { get; set; }

    [InverseProperty(nameof(Order.Package))]
    public virtual ICollection<Order> Orders { get; set; }
}