using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common.Interfaces;

namespace EcoFarm.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    [Column("CODE")] public string Code { get; set; }
    [Column("NAME")] public string Name { get; set; }
    [Column("CREATED_BY")] public string CreatedBy { get; set; }
    [Column("CREATED_DATE")] public DateTime? CreatedDate { get; set; }
    [Column("UPDATED_BY")] public string UpdatedBy { get; set; }
    [Column("UPDATED_DATE")] public DateTime? UpdatedDate { get; set; }
}