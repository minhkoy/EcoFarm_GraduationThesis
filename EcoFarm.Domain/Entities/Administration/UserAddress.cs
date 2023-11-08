using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

[Table("USER_ADDRESS")]
public class UserAddress : BaseNonExtendedEntity 
{
    public string USER_ID { get; set; }
    public string ADDRESS { get; set; }
    public string PHONE_NUMBER { get; set; }
    public bool IS_MAIN { get; set; }


}