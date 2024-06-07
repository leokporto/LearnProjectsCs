using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Shared.Entities
{
	public interface IAggregateRoot
	{
		void Apply(IDomainEvent @event);
	}
}
