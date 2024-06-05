using EventSourcingDemo.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Shared.Events
{
	public abstract class BaseEvent : IDomainEvent 
	{
		public BaseEvent()
		{
			CreatedAtUtc = DateTime.UtcNow;
		}

		public abstract Guid StreamId { get; }

		public DateTime CreatedAtUtc { get; }

	}

}
