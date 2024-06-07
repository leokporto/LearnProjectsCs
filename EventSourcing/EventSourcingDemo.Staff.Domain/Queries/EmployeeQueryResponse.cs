using EventSourcingDemo.Shared.Enums;
using EventSourcingDemo.Shared.Queries;

namespace EventSourcingDemo.Staff.Domain.Queries
{
	public class EmployeeQueryResponse : IQueryResponse
	{
		public required eResponseStatus Status { get; init; }

		public string? Message { get; init; }

		public object? Data { get; init; }
	}
}
