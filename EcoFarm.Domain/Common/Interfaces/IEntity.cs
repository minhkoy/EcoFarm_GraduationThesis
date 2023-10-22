namespace EcoFarm.Domain.Common.Interfaces;

public interface IEntity
{
    string Id { get; set; }
    int? Version { get; set; } 
    DateTime CreatedTime { get; set; }
    DateTime? ModifiedTime { get; set; }
    string CreatedBy { get; set; }
    string ModifiedBy { get; set; }
    bool? IsActive { get; set; }
    bool? IsDelete { get; set; }
}