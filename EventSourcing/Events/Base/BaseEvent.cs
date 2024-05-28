using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Events.Base
{
	public abstract class BaseEvent
	{
		public abstract Guid StreamId { get; }

		public long CreatedAtUtcTicks { get; set; }
	}
}
