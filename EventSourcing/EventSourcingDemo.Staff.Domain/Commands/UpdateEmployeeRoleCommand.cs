using EventSourcingDemo.Shared.Commands;
using EventSourcingDemo.Staff.Domain.Enums;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class UpdateEmployeeRoleCommand : ICommandRequest
	{
		public required eRoleOperation Operation { get; init; }

		/// <summary>
		/// Gets or sets the employee identifier.
		/// </summary>
		/// <value>The employee identifier.</value>
		public required Guid EmployeeId { get; init; }

		public string RoleName { get; init; }
	}
}
