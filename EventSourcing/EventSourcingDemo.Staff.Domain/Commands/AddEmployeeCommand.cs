using EventSourcingDemo.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class AddEmployeeCommand : ICommandRequest
	{
		/// <summary>
		/// Gets or sets the employee ID.
		/// </summary>
		/// <value>The employee ID.</value>
		public required Guid EmployeeId { get; init; }

		/// <summary>
		/// Gets or sets the full name of the employee.
		/// </summary>
		/// <value>The full name of the employee.</value>
		public required string FullName { get; init; }

		/// <summary>
		/// Gets or sets the date of birth of the employee.
		/// </summary>
		/// <value>The date of birth of the employee.</value>
		public required DateTime DateOfBirth { get; init; }
	}
}
