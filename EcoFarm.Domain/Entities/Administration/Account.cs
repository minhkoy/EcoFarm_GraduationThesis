using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Common.Values.Constants;
using Microsoft.EntityFrameworkCore;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ACCOUNT")]
[Index(nameof(USERNAME), IsUnique = true)]

public class Account : BaseNonExtendedEntity
{
    public string NAME { get; set; }
    public string USERNAME { get; set; }
    public string EMAIL { get; set; }
    public string AVATAR_URL { get; set; }
    /// <summary>
    /// User password salt
    /// </summary>
    public string SALT { get; set; }
    public bool IS_EMAIL_CONFIRMED { get; set; } = true; //XXX
    [JsonIgnore]
    public string HASHED_PASSWORD { get; set; }
    //public DateTime? DATE_OF_BIRTH { get; set; }
    public DateTime? LAST_LOGGED_IN { get; set; }
    public DateTime? LAST_LOGGED_OUT { get; set; }
    public AccountType ACCOUNT_TYPE { get; set; }
    //[Column("")]
    public string LOCKED_REASON { get; set; }
    public string LATEST_GENERATED_TOKEN { get; set; }
    //Extended properties
    [NotMapped]
    public string ACCOUNT_TYPE_NAME { get => EFX.AccountTypes.dctAccountType[ACCOUNT_TYPE]; }

    //Inverse properties
    
    [InverseProperty(nameof(SellerEnterprise.AccountInfo))]
    public virtual SellerEnterprise SellerEnterpriseInfo { get; set; }
    [InverseProperty(nameof(User.AccountInfo))]
    public virtual User UserInfo { get; set; }
    [InverseProperty(nameof(AccountVerify.RequestAccount))]
    public virtual ICollection<AccountVerify> AccountVerifies { get; set; }
    [InverseProperty(nameof(RoleUser.UserOfRole))]
    public virtual ICollection<RoleUser> RoleUsers { get; set; }
    [InverseProperty(nameof(Notification.FromAccount))]
    public virtual ICollection<Notification> FromNotifications { get; set; }

    [InverseProperty(nameof(NotificationAccount.UserInfo))]
    public virtual ICollection<NotificationAccount> NotificationUsers { get; set; }
    //[InverseProperty(nameof(Notification.ToAccount))]
    //public virtual ICollection<Notification> ToNotifications { get; set; }
}