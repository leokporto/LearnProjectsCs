using EventSourcingDemo.Shared.Commands;
using EventSourcingDemo.Staff.Domain.Entities;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class AddEmployeeCommand : EmployeeCommandBase, ICommandRequest
	{
		/// <summary>
		/// Gets or sets the date of birth of the employee.
		/// </summary>
		/// <value>The date of birth of the employee.</value>
		public required DateTime DateOfBirth { get; init; }
	}
}
