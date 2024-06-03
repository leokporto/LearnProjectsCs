using EventSourcing.Events.Base;
using System;

namespace EventSourcing.Events.StudentEvents
{
	public class StudentUnEnrolled : BaseEvent
	{
		public  Guid StudentId { get; set; }

		public  string CourseName { get; set; }

		public override Guid StreamId => StudentId;
	}
}
