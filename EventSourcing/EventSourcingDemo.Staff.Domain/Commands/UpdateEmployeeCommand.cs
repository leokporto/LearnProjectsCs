using EventSourcingDemo.Shared.Commands;
using EventSourcingDemo.Staff.Domain.Entities;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class UpdateEmployeeCommand : EmployeeCommandBase, ICommandRequest
	{
		/// <summary>
		/// Gets or sets the employee identifier.
		/// </summary>
		/// <value>The employee identifier.</value>
		public required Guid EmployeeId { get; init; }

		public string Email { get; set; }
	}
}
