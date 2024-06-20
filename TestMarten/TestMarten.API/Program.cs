using Marten;
using TestMarten.API.Extensions;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This is the absolute, simplest way to integrate Marten into your
// .NET application with Marten's default configuration
builder.Services.AddMarten(options =>
{
	// Establish the connection string to your Marten database
	options.Connection(builder.Configuration["ConnectionStrings:MartenDb"]!);

	// Specify that we want to use STJ as our serializer
	options.UseSystemTextJsonForSerialization();

	// If we're running in development mode, let Marten just take care
	// of all necessary schema building and patching behind the scenes
	if (builder.Environment.IsDevelopment())
	{
		options.AutoCreateSchemaObjects = AutoCreate.All;
	}
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapUserEndpoints();

app.Run();
