using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common.Interfaces;

namespace EcoFarm.Domain.Common;

public abstract class BaseEntity : BaseNonExtendedEntity, IAuditableEntity
{
    [Column("CODE")] public string CODE { get; set; }
    [Column("NAME")] public string NAME { get; set; }
}