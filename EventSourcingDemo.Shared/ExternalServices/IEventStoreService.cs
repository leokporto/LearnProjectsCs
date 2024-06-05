using EventSourcingDemo.Shared.Entities;
using EventSourcingDemo.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
