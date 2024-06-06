using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Staff.Domain.Events
{
	public class EmployeeDismissedEvent : BaseEvent
	{
		private Guid _employeeId;
		public EmployeeDismissedEvent(Guid employeeId)
		{
			_employeeId = employeeId;

		}

		public override Guid StreamId => _employeeId;

		public bool IsActive => false;
	}
}
