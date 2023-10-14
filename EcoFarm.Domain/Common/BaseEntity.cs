using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common.Interfaces;

namespace EcoFarm.Domain.Common;

public abstract class BaseEntity : IEntity
{
    private readonly List<BaseEvent> _domainEvents = new();
    
    [Key]
    [Column("ID")]
    public string Id { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    [Column("VERSION")] 
    public int? Version { get; set; } = 1;
    [Column("CREATED_TIME")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;
    [Column("MODIFIED_TIME")]
    public DateTime? ModifiedTime { get; set; }
    [Column("IS_ACTIVE")]
    public bool? IsActive { get; set; }
    [Column("IS_DELETE")]
    public bool? IsDelete { get; set; }
    
    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();
 
    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear(); 
}