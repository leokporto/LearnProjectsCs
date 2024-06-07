using EventSourcingDemo.Staff.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddOpenAPIServices();
builder.AddStaffServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapStaffEndpoints();

app.Run();

