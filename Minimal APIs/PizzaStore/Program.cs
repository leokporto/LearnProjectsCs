var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext()
       .AddSwaggerService();

var app = builder.Build();

app.UseSwaggerService();

app.MapPizzaRoutes();

app.Run();
