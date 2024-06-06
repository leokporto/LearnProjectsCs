using EventSourcingDemo.Shared.Events;

namespace EventSourcingDemo.Staff.Domain.Events
{
	public class EmployeeAddedEvent : BaseEvent
	{
		

		public override Guid StreamId => EmployeeId;

		/// <summary>
		/// Gets or sets the employee ID.
		/// </summary>
		/// <value>The employee ID.</value>
		public required Guid EmployeeId { get; init; }

		/// <summary>
		/// Gets or sets the full name of the employee.
		/// </summary>
		/// <value>The full name of the employee.</value>
		public required string FullName { get; set; }

		/// <summary>
		/// Gets or sets the date of birth of the employee.
		/// </summary>
		/// <value>The date of birth of the employee.</value>
		public required DateTime DateOfBirth { get; init; }

		/// <summary>
		/// Gets a value indicating whether the employee is active.
		/// </summary>
		/// <value><c>true</c> if the employee is active; otherwise, <c>false</c>.</value>
		public required bool IsActive { get; init; }
	}
}
