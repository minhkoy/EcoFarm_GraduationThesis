using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Constants;
using Microsoft.EntityFrameworkCore;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ACCOUNT")]
[Index(nameof(USERNAME))]
[Index(nameof(EMAIL))]

public class Account : BaseEntity
{
    
    public string USERNAME { get; set; }
    public string EMAIL { get; set; }
    public bool IS_EMAIL_CONFIRMED { get; set; } = false;
    [JsonIgnore]
    public string HASHED_PASSWORD { get; set; }
    public DateTime? DATE_OF_BIRTH { get; set; }
    public DateTime? LAST_LOGGED_IN { get; set; }
    public DateTime? LAST_LOGGED_OUT { get; set; }
    public RoleType ROLE { get; set; }
    //[Column("")]

    //Extended properties
    [NotMapped]
    public string ROLE_NAME { get => EFX.Roles.dctRoles[ROLE]; }

    //Inverse properties
    [InverseProperty(nameof(Order.UserOrdered))]
    public virtual ICollection<Order> Orders { get; set; }
    [InverseProperty(nameof(SellerEnterprise.UserRelated))]
    public virtual SellerEnterprise SellerEnterpriseInfo { get; set; }
    [InverseProperty(nameof(AccountVerify.RequestAccount))]
    public virtual ICollection<AccountVerify> AccountVerifies { get; set; }
}