using EventSourcing.Events.Base;
using System;

namespace EventSourcing.Events.StudentEvents
{
	public class StudentUpdated : BaseEvent
	{
		public Guid StudentId { get; set; }

		public  string FullName { get; set; }

		public  string Email { get; set; }

		public override Guid StreamId => StudentId;
	}
}
