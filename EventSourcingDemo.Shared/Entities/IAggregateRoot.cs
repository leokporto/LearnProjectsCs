using EventSourcingDemo.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Shared.Entities
{
	public interface IAggregateRoot
	{
		void Apply(IDomainEvent @event);
	}
}
