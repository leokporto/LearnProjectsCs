using EventSourcing.Events.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Events.StudentEvents
{
	public class StudentEnrolled : BaseEvent
	{
		public Guid StudentId { get; set; }

		public string CourseName { get; set; }

		public override Guid StreamId => StudentId;
	}
}
