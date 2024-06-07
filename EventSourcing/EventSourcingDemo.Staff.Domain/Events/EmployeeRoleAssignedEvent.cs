using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Staff.Domain.Events
{
	public class EmployeeRoleAssignedEvent : BaseEvent
	{
		private Guid _employeeId;
		public EmployeeRoleAssignedEvent(Guid employeeId, string roleName)
		{
			_employeeId = employeeId;
			RoleName = roleName;
		}

		public override Guid StreamId => _employeeId;

		public string RoleName { get; }
	}
}
