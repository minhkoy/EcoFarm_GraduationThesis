using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

[Table("USER_ADDRESS")]
public class UserAddress : BaseNonExtendedEntity 
{
    [Column("USER_ID")]
    public string UserId { get; set; }
    [Column("ADDRESS")]
    public string Address { get; set; }
    [Column("PHONE_NUMBER")]
    public string PhoneNumber { get; set; }
}