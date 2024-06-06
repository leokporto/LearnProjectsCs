using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Staff.Domain.Events
{
	public class EmployeeRoleRemovedEvent : BaseEvent
	{
		private Guid _employeeId;
		public EmployeeRoleRemovedEvent(Guid employeeId)
		{
			_employeeId = employeeId;
		}

		public override Guid StreamId => _employeeId;
		
		public required string RoleName { get; set; }
	}
}
