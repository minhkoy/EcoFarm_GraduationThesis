using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common;

namespace EcoFarm.Domain.Entities;

[Table("ECO_SERVICE")]
public class EcoService : BaseAuditableEntity
{
    [Column("DESCRIPTION")] public string Description { get; set; }
    [Column("SUPPLIER_ACCOUNT_ID")] public string SupplierAccountId { get; set; }
    [Column("START_TIME")] public DateTime StartTime { get; set; }
    [Column("END_TIME")] public DateTime? EndTime { get; set; }
    [Column("QUANTITY_START")] public int? QuantityStart { get; set; }
    [Column("QUANTITY_SOLD")] public int QuantitySold { get; set; }
    [Column("SERVICE_TYPE")] public int ServiceType { get; set; }
}