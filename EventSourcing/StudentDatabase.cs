using EventSourcing.Events.Base;
using EventSourcing.Events.StudentEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
	public class StudentDatabase
	{
		private readonly Dictionary<Guid,SortedList<DateTimeOffset, BaseEvent>> _studentEvents = new Dictionary<Guid, SortedList<DateTimeOffset, BaseEvent>>();
		private readonly Dictionary<Guid, Student> _students= new Dictionary<Guid, Student>();

		public void Append(BaseEvent @event)
		{
			SortedList<DateTimeOffset, BaseEvent> stream = null;
			if(!_studentEvents.ContainsKey(@event.StreamId))
			{
				stream = new SortedList<DateTimeOffset, BaseEvent>();
				_studentEvents.Add(@event.StreamId, stream);
			}
			
			_studentEvents[@event.StreamId].Add(@event.CreatedAtUtc, @event);

			_students[@event.StreamId] = Find(@event.StreamId);
		}

		public Student GetStudentView(Guid studentId)
		{
			if (!_students.ContainsKey(studentId))
			{
				return null;
			}

			return _students[studentId];
		}

		public Student Find(Guid studentId)
		{
			if(!_studentEvents.ContainsKey(studentId))
			{
				return null;
			}

			var student = new Student();
			var studentEvents = _studentEvents[studentId];

			foreach (var studentEvent in studentEvents.Values)
			{
				student.Apply(studentEvent);
			}
			return student;
		}
	}
}
