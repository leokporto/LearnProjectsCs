using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Shared.ExternalServices;
using EventSourcingDemo.Shared.Queries;
using EventSourcingDemo.Staff.Domain.Commands;
using EventSourcingDemo.Staff.Domain.Queries;
using EventSourcingDemo.Staff.Infra.ExternalServices;

namespace EventSourcingDemo.Staff.API.Extensions
{
	public static class ServicesExtensions
	{
		public static void AddOpenAPIServices(this WebApplicationBuilder builder)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
		}

		public static void AddStaffServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<IEventStoreService, StaffMemoryEventStore>();
			builder.Services.AddTransient<IQueryHandler<GetEmployeeQuery>, EmployeeQueriesHandler>();
			builder.Services.AddTransient<IQueryHandler<GetEmployeeEventsQuery>, EmployeeQueriesHandler>();
			builder.Services.AddTransient<ICommandHandler<AddEmployeeCommand>, EmployeeCommandHandler>();
			builder.Services.AddTransient<ICommandHandler<UpdateEmployeeCommand>, EmployeeCommandHandler>();
			builder.Services.AddTransient<ICommandHandler<UpdateEmployeeRoleCommand>, EmployeeCommandHandler>();
			builder.Services.AddTransient<ICommandHandler<DismissEmployeeCommand>, EmployeeCommandHandler>();
		}
	}
}
