using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;

[Table("SELLER_ENTERPRISE")]
public class SellerEnterprise : BaseNonExtendedEntity 
{
    public string NAME { get; set; }
    public string ACCOUNT_ID { get; set; }
    public string DESCRIPTION { get; set; }
    public string TAX_CODE { get; set; }
    public bool? IS_APPROVED { get; set; } = true;
    public string APPROVED_OR_REJECTED_BY { get; set; }
    public DateTime? APPROVED_OR_REJECTED_TIME { get; set; }
    public string REJECT_REASON { get; set; }
    public string HOTLINE { get; set; }
    public string ADDRESS { get; set; }
    public string AVATAR_URL { get; set; }

    //Inverse properties
    [ForeignKey(nameof(ACCOUNT_ID))]
    [InverseProperty(nameof(Account.SellerEnterpriseInfo))]
    public virtual Account AccountInfo { get; set; }

    [InverseProperty(nameof(FarmingPackage.Enterprise))]
    public virtual ICollection<FarmingPackage> EnterpiseServicePackages { get; set; }
    [InverseProperty(nameof(Order.EnterpriseInfo))]
    public virtual ICollection<Order> Orders { get; set; }
    [InverseProperty(nameof(Product.Enterprise))]
    public virtual ICollection<Product> Products { get; set; }
}