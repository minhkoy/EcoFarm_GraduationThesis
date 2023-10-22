using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common.Interfaces;

namespace EcoFarm.Domain.Common;

public abstract class BaseEntity : BaseNonExtendedEntity, IAuditableEntity
{
    [Column("CODE")] public string Code { get; set; }
    [Column("NAME")] public string Name { get; set; }
}