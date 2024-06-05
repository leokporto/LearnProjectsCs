using EventSourcingDemo.Shared.Entities;
using EventSourcingDemo.Shared.Events;
using EventSourcingDemo.Shared.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Staff.Infra.ExternalServices
{
	public class StaffMemoryEventStore : IEventStoreService
	{
		private readonly Dictionary<Guid, SortedList<DateTimeOffset, IDomainEvent>> _events = new Dictionary<Guid, SortedList<DateTimeOffset, IDomainEvent>>();

		public void Append(IDomainEvent @event)
		{
			SortedList<DateTimeOffset, IDomainEvent> stream = null;
			if (!_events.ContainsKey(@event.StreamId))
			{
				stream = new SortedList<DateTimeOffset, IDomainEvent>();
				_events.Add(@event.StreamId, stream);
			}

			_events[@event.StreamId].Add(@event.CreatedAtUtc, @event);
		}

		public Task AppendAsync(IDomainEvent @event)
		{
			SortedList<DateTimeOffset, IDomainEvent> stream = null;
			if (!_events.ContainsKey(@event.StreamId))
			{
				stream = new SortedList<DateTimeOffset, IDomainEvent>();
				_events.Add(@event.StreamId, stream);
			}

			return Task.Run(() => _events[@event.StreamId].Add(@event.CreatedAtUtc, @event));
		}

		public IAggregateRoot Find<T>(Guid streamId) where T : IAggregateRoot, new()
		{
			
			if (!_events.ContainsKey(streamId))
			{
				return null;
			}

			var aggregate = new T();
			var events = _events[streamId];

			foreach (var item in events.Values)
			{
				aggregate.Apply(item);
			}
			return aggregate;
			
		}

		public SortedList<DateTimeOffset, IDomainEvent> ListStreamEvents(Guid streamId)
		{
			if (!_events.ContainsKey(streamId))
			{
				return null;
			}

			return _events[streamId];
		}
	}
}
