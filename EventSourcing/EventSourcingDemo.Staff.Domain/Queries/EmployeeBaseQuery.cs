using EventSourcingDemo.Shared.Queries;

namespace EventSourcingDemo.Staff.Domain.Queries
{
	public abstract class EmployeeBaseQuery : IQueryRequest
	{
		public Guid EmployeeId { get; set; }
	}
}
