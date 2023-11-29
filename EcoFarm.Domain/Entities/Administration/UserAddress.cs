using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities.Administration;

[Table("USER_ADDRESS")]
public class UserAddress : BaseNonExtendedEntity 
{
    public string USER_ID { get; set; }
    public string RECEIVER_NAME { get; set; }
    public string ADDRESS { get; set; }
    public string DESCRIPTION { get; set; }
    public string PHONE_NUMBER { get; set; }
    public bool IS_MAIN { get; set; }
    [ForeignKey(nameof(USER_ID))]
    [InverseProperty(nameof(User.Addresses))]
    public virtual User UserInfo { get; set; }

}