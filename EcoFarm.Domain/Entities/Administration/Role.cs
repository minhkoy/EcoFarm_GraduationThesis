using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ROLE")]
public class Role : BaseEntity
{
    public string DESCRIPTION { get; set; }
    [InverseProperty(nameof(RoleUser.RoleOfUser))]
    public virtual ICollection<RoleUser> RoleUsers { get; set; }
}