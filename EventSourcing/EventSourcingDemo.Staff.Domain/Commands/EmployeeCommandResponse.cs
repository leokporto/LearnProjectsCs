using EventSourcingDemo.Shared.Commands;
using EventSourcingDemo.Shared.Enums;

namespace EventSourcingDemo.Staff.Domain.Commands
{
	public class EmployeeCommandResponse : ICommandResponse
	{
		public required eResponseStatus Status { get; init; }

		public string? Message { get; init; }
	}
}
