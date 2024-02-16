using PizzaStore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext()
       .AddIOCServices()
       .AddSwaggerService();

var app = builder.Build();

app.UseSwaggerService();

app.MapPizzaRoutes()
    .MapAuthRoutes();
    

app.Run();
