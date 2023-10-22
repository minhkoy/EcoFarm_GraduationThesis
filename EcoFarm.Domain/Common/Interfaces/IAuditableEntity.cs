namespace EcoFarm.Domain.Common.Interfaces;

public interface IAuditableEntity : IEntity
{
    string Code { get; set; }
    string Name { get; set; }
}