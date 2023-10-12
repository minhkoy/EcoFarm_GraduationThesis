namespace EcoFarm.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    string Code { get; set; }
    string Name { get; set; }
    string CreatedBy { get; set; }
    DateTime? CreatedDate { get; set; }
    string UpdatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
}