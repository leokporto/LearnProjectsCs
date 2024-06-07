using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Shared.Enums;
using EventSourcingDemo.Shared.Queries;
using EventSourcingDemo.Staff.Domain.Commands;
using EventSourcingDemo.Staff.Domain.Entities;
using EventSourcingDemo.Staff.Domain.Queries;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventSourcingDemo.Staff.API.Extensions
{
	public static class StaffEndpointsExtension
	{
		public static void MapStaffEndpoints(this WebApplication app)
		{
			var group = app.MapGroup("/staff").WithOpenApi();

			group.MapGet("/{@id:guid}", GetEmployee).WithName("GetEmployee");
			group.MapGet("/{@id:guid}/events", GetEmployeeEvents).WithName("GetEmployeeEvents");
			group.MapPost("/", AddEmployee).WithName("AddEmployee");
			group.MapPut("/", UpdateEmployee).WithName("UpdateEmployee");
			group.MapPut("/roles", UpdateEmployeeRole).WithName("UpdateEmployeeRole");
			group.MapPut("/dismiss", DismissEmployee).WithName("DismissEmployee");
		}

		private static Results<Ok<IQueryResponse>, NotFound> GetEmployee(Guid employeeId, IQueryHandler<GetEmployeeQuery> getEmployeeQuery)
		{
			var request = new GetEmployeeQuery() { EmployeeId = employeeId };
			var employee = getEmployeeQuery.Handle(request);
			if (employee == null)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.Ok(employee);
		}

		private static Results<Ok<IQueryResponse>, NotFound> GetEmployeeEvents(Guid employeeId, IQueryHandler<GetEmployeeEventsQuery> getEmployeeEventsQuery)
		{
			var request = new GetEmployeeEventsQuery() { EmployeeId = employeeId };
			var employeeEvents = getEmployeeEventsQuery.Handle(request);
			if (employeeEvents == null)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.Ok(employeeEvents);
		}

		private static async Task<Results<Created, BadRequest>> AddEmployee(AddEmployeeCommand request, ICommandHandler<AddEmployeeCommand> addEmployeeCmd)
		{
			
			var response = await addEmployeeCmd.HandleAsync(request);

			if(response.Status == eResponseStatus.Failure)
			{
				return TypedResults.BadRequest();
			}
			return TypedResults.Created();
		}

		private static async Task<Results<NoContent,NotFound>> UpdateEmployee(UpdateEmployeeCommand request, ICommandHandler<UpdateEmployeeCommand> updateEmployeeCmd)
		{
			var response = await updateEmployeeCmd.HandleAsync(request);
			if (response.Status == eResponseStatus.Failure)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.NoContent();
		}

		private static async Task<Results<NoContent, NotFound>> UpdateEmployeeRole(UpdateEmployeeRoleCommand request, ICommandHandler<UpdateEmployeeRoleCommand> updateRoleCmd)
		{
			var response = await updateRoleCmd.HandleAsync(request);
			if(response.Status == eResponseStatus.Failure)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.NoContent();
		}

		private static async Task<Results<NoContent, NotFound>> DismissEmployee(DismissEmployeeCommand request, ICommandHandler<DismissEmployeeCommand> dismissEmployeeCmd)
		{
			var response = await dismissEmployeeCmd.HandleAsync(request);
			if (response.Status == eResponseStatus.Failure)
			{
				return TypedResults.NotFound();
			}
			return TypedResults.NoContent();
		}
	}
}
