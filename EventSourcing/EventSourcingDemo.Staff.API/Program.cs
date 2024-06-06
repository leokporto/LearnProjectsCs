using EventSourcingDemo.Shared.Commands.Handlers;
using EventSourcingDemo.Staff.Domain.Commands;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<ICommandHandler<AddEmployeeCommand>, EmployeeCommandHandler>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var group = app.MapGroup("/staff").WithOpenApi();

group.MapPost("/", (AddEmployeeCommand employee) =>
{
	//addEmployeeCmd.HandleAsync(employee); 
})
.WithName("AddEmployee")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
