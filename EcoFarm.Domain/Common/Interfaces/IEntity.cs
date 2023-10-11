namespace EcoFarm.Domain.Common.Interfaces;

public interface IEntity
{
    string Id { get; set; }
    long CreatedTime { get; set; }
    long ModifiedTime { get; set; }
    bool? IsActive { get; set; }
    bool? IsDelete { get; set; }
}