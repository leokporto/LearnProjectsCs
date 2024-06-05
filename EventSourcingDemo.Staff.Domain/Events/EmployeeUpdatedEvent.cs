using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Staff.Domain.Events
{
	public class EmployeeUpdatedEvent : BaseEvent
	{
		private Guid _employeeId;

		public EmployeeUpdatedEvent(Guid emplyeeId)
		{
			_employeeId = emplyeeId;
		}

		public override Guid StreamId => _employeeId;

		/// <summary>
		/// Gets or sets the full name of the employee.
		/// </summary>
		/// <value>The full name of the employee.</value>
		public required string FullName { get; set; }

		public required string Email { get; set; }
	}
}
