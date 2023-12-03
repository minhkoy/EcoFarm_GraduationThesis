using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EcoFarm.Domain.Common.Interfaces;
using EcoFarm.Domain.Common.Values.Constants;

namespace EcoFarm.Domain.Common;

public abstract class BaseNonExtendedEntity : IEntity
{
    private readonly List<BaseEvent> _domainEvents = new();

    [Key] [Column("ID")] public string ID { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
    [Column("VERSION"), ConcurrencyCheck] public Guid VERSION { get; set; }
    [Column("CREATED_TIME")] public DateTime CREATED_TIME { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(EFX.Timezone_VN));
    [Column("CREATED_BY")] public string CREATED_BY { get; set; }
    [Column("MODIFIED_TIME")] public DateTime? MODIFIED_TIME { get; set; }
    [Column("MODIFIED_BY")] public string MODIFIED_BY { get; set; }
    [Column("IS_ACTIVE")] public bool IS_ACTIVE { get; set; } = true;
    [Column("IS_DELETE")] public bool IS_DELETE { get; set; } = false;

    [NotMapped] public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(BaseEvent domainEvent) => _domainEvents.Remove(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}