using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;

[Table("SELLER_ENTERPRISE")]
public class SellerEnterprise : BaseEntity 
{
    [Column("DESCRIPTION")]
    public string Description { get; set; }
    [Column("TAX_CODE")]
    public string TaxCode { get; set; }
    [Column("IS_APPROVED")]
    public bool IsApproved { get; set; } = false;
    [Column("HOTLINE")]
    public string Hotline { get; set; }
    [Column("ADDRESS")]
    public string Address { get; set; }
    //[Column("")]
    
    //Inverse properties
    [InverseProperty(nameof(ServicePackage))]
    public virtual List<ServicePackage> EnterpiseServicePackages { get; set; }
}