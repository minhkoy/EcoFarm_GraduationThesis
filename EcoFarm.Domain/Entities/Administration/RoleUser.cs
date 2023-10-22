using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ROLE_USER")]
public class RoleUser : BaseNonExtendedEntity
{
    [Column("ROLE_ID")] public string RoleId { get; set; }
    [Column("USER_ID")] public string UserId { get; set; }
    
}