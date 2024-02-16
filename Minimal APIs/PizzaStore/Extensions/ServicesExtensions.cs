using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Services;

public static class ServicesExtensions
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("PizzaStore") ?? "Data Source=PizzaStore.db";
        builder.Services.AddSqlite<PizzaDb>(connectionString);
        
        return builder;
    }

    public static WebApplicationBuilder AddSwaggerService(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pizzastore API", Description = "MAking the pizzas you love", Version = "v1" });
        });

        return builder;
    }

    public static WebApplicationBuilder AddIOCServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddTransient<TokenService>();

		return builder;
	}
    
}
