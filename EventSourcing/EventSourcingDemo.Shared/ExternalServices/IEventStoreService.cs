using EventSourcingDemo.Shared.Entities;
using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Shared.ExternalServices
{
	public interface IEventStoreService
	{
		void Append(IDomainEvent @event);

		Task AppendAsync(IDomainEvent @event);

		SortedList<DateTimeOffset, IDomainEvent> ListStreamEvents(Guid streamId);

		IAggregateRoot Find<T>(Guid streamId) where T : IAggregateRoot, new();


	}
}
