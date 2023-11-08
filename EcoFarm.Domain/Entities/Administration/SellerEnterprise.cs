using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;
using EcoFarm.Domain.Entities.Administration;

[Table("SELLER_ENTERPRISE")]
public class SellerEnterprise : BaseEntity 
{
    public string USER_ID { get; set; }
    public string DESCRIPTION { get; set; }
    public string TAX_CODE { get; set; }
    public bool? IS_APPROVED { get; set; }
    public string HOTLINE { get; set; }
    public string ADDRESS { get; set; }

    //Inverse properties
    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(Account.SellerEnterpriseInfo))]
    public virtual Account UserRelated { get; set; }

    [InverseProperty(nameof(ServicePackage.Enterprise))]
    public virtual List<ServicePackage> EnterpiseServicePackages { get; set; }
}