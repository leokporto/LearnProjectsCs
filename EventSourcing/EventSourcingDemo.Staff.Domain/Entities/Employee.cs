using EventSourcingDemo.Shared.Entities;
using EventSourcingDemo.Shared.Events;
using EventSourcingDemo.Staff.Domain.Events;

namespace EventSourcingDemo.Staff.Domain.Entities
{
	public class Employee : IAggregateRoot
	{
		public Employee() 
		{ 
		
		}

		public Employee(string fullName, string email, DateTime dateOfBirth)
		{
			FullName = fullName;
			Email = email;
			DateOfBirth = dateOfBirth;
		}


		public Guid Id { get; private set; }

		public string FullName { get; private set; }

		public string Email { get; private set; }

		public bool IsActive { get; private set; }

		public List<string> Roles { get; private set; } = new List<string>();

		public DateTime DateOfBirth { get; private set; }

		private void Apply(EmployeeAddedEvent employeeHired)
		{
			Id = employeeHired.EmployeeId;
			FullName = employeeHired.FullName;
			DateOfBirth = employeeHired.DateOfBirth;
			IsActive = employeeHired.IsActive;
		}

		private void Apply(EmployeeUpdatedEvent employeeUpdated)
		{
			FullName = employeeUpdated.FullName;			
			Email = employeeUpdated.Email;
		}

		private void Apply(EmployeeRoleAssignedEvent roleAssigned)
		{
			if (!Roles.Contains(roleAssigned.RoleName))
			{
				Roles.Add(roleAssigned.RoleName);
			}
		}

		private void Apply(EmployeeRoleRemovedEvent roleRemoved)
		{
			if (Roles.Contains(roleRemoved.RoleName))
			{
				Roles.Remove(roleRemoved.RoleName);
			}
		}

		private void Apply(EmployeeDismissedEvent employeeDismissed)
		{
			IsActive = employeeDismissed.IsActive;
		}

		public void Apply(IDomainEvent evt)
		{
			switch (evt)
			{
				case EmployeeAddedEvent employeeHired:
					Apply(employeeHired);
					break;		
				case EmployeeUpdatedEvent employeeUpdated:
					Apply(employeeUpdated);
					break;
				case EmployeeRoleAssignedEvent employeeRoleAssigned:
					Apply(employeeRoleAssigned);
					break;
				case EmployeeRoleRemovedEvent employeeRoleRemoved:
					Apply(employeeRoleRemoved);
					break;
				case EmployeeDismissedEvent employeeDismissed:
					Apply(employeeDismissed);
					break;
			}
		}

	}
}
