using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Enums;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities;

[Table("SERVICE_PACKAGE")]
public class ServicePackage : BaseEntity
{
    public string DESCRIPTION { get; set; }
    public string SELLER_ENTERPRISE_ID { get; set; }
    public DateTime START_TIME { get; set; }
    public DateTime? END_TIME { get; set; }
    public int? QUANTITY_START { get; set; }
    public int QUANTITY_SOLD { get; set; } = 0;
    //Considering, may be used for registering the service
    [NotMapped]
    public decimal? DEPOSIT { get; set; }
    public decimal PRICE { get; set; } = 0;
    public decimal? DISCOUNT_PRICE { get; set; }
    public DateTime? DISCOUNT_START { get; set; }
    public DateTime? DISCOUNT_END { get; set; }
    public ServicePackageApprovalStatus STATUS { get; set; } = ServicePackageApprovalStatus.Pending;
    public ServiceType SERVICE_TYPE { get; set; }
    public string REJECT_REASON { get; set; }

    //Extension properties
    [NotMapped] public int QuantityRemain => !QUANTITY_START.HasValue ? 0 : QUANTITY_START.Value - QUANTITY_SOLD;
    //Inverse properties
    [ForeignKey(nameof(SELLER_ENTERPRISE_ID))]
    [InverseProperty(nameof(SellerEnterprise.EnterpiseServicePackages))]
    public virtual SellerEnterprise Enterprise { get; set; }

    [InverseProperty(nameof(Order.Package))]
    public virtual ICollection<Order> Orders { get; set; }
    [InverseProperty(nameof(Product.Service))]
    public virtual ICollection<Product> Products { get; set; }
    [InverseProperty(nameof(CartDetail.Service))]
    public virtual ICollection<CartDetail> CartDetails { get; set; }
    [InverseProperty(nameof(ServiceImage.Service))]
    public virtual ICollection<ServiceImage> ServiceImages { get; set; }
}