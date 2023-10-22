using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Enums;

namespace EcoFarm.Domain.Entities;

[Table("SERVICE_PACKAGE")]
public class ServicePackage : BaseEntity
{
    [Column("DESCRIPTION")] public string Description { get; set; }
    [Column("SELLER_ENTERPRISE_ID")] public string SellerEnterpriseId { get; set; }
    [Column("START_TIME")] public DateTime StartTime { get; set; }
    [Column("END_TIME")] public DateTime? EndTime { get; set; }
    [Column("QUANTITY_START")] public int? QuantityStart { get; set; }
    [Column("QUANTITY_SOLD")] public int QuantitySold { get; set; }
    [Column("SERVICE_TYPE")] public int ServiceType { get; set; }
    [Column("PRICE")] public double Price { get; set; } = 0;
    [Column("STATUS")] public int Status { get; set; } = (int)HelperEnums.ServicePackageApprovalStatus.Pending;

    //Extension properties
    [NotMapped] public int QuantityRemain => !QuantityStart.HasValue ? 0 : QuantityStart.Value - QuantitySold;
    //Inverse properties
    [ForeignKey(nameof(SellerEnterpriseId))]
    [InverseProperty(nameof(SellerEnterprise.EnterpiseServicePackages))]
    public virtual SellerEnterprise Enterprise { get; set; }

    [InverseProperty(nameof(Order.Package))]
    public virtual ICollection<Order> Orders { get; set; }
}