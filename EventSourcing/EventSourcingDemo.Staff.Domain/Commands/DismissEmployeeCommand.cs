using EventSourcingDemo.Shared.Commands;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class DismissEmployeeCommand : ICommandRequest
	{
		/// <summary>
		/// Gets or sets the employee identifier.
		/// </summary>
		/// <value>The employee identifier.</value>
		public required Guid EmployeeId { get; init; }
	}
}
