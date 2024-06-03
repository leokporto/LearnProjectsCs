using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Events.Base
{
	/// <summary>
	/// Represents a base class for events in the event sourcing system.
	/// </summary>
	public abstract class BaseEvent
	{
		/// <summary>
		/// Gets the unique identifier of the event stream.
		/// </summary>
		public abstract Guid StreamId { get; }

		/// <summary>
		/// Gets or sets the ticks representing the creation time of the event in UTC.
		/// </summary>
		public DateTimeOffset CreatedAtUtc { get; set; }
	}
}
