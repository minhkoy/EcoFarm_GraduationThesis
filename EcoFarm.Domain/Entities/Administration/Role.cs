using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities.Administration;

[Table("ROLE")]
public class Role : BaseEntity
{
    [Column("DESCRIPTION")]
    public string Description { get; set; }
}