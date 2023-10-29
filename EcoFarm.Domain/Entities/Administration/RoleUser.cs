using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ROLE_USER")]
public class RoleUser : BaseNonExtendedEntity
{
    public string ROLE_ID { get; set; }
    public string USER_ID { get; set; }
    
}