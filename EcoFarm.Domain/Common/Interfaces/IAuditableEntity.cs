namespace EcoFarm.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    string CODE { get; set; }
    string NAME { get; set; }
}