namespace EventSourcingDemo.Staff.Domain.Entities
{
	public class EmployeeCommandBase
	{
		/// <summary>
		/// Gets or sets the full name of the employee.
		/// </summary>
		/// <value>The full name of the employee.</value>
		public required string FullName { get; init; }

		
	}
}
