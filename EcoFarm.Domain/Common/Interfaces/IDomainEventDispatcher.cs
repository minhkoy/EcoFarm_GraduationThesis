namespace EcoFarm.Domain.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseNonExtendedEntity> entitiesWithEvents);
}