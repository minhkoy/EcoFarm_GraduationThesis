using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;

[Table("SELLER_ENTERPRISE")]
public class SellerEnterprise : BaseEntity 
{
    public string DESCRIPTION { get; set; }
    public string TAX_CODE { get; set; }
    public bool IS_APPROVED { get; set; } = false;
    public string HOTLINE { get; set; }
    public string ADDRESS { get; set; }
    
    //Inverse properties
    [InverseProperty(nameof(ServicePackage))]
    public virtual List<ServicePackage> EnterpiseServicePackages { get; set; }
}