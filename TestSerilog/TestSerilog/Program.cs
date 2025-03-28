using TestSerilog.Contracts;
using TestSerilog.Data;
using TestSerilog.Services;

namespace TestSerilog;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        string connString = builder.Configuration.GetConnectionString("BeersDbConnection");

        builder.Services.AddScoped<IBeerRepository>((sp) => new BeerRepository(connString));
        builder.Services.AddScoped<IBeerService, BeerService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        

        app.MapGet("/beers", (HttpContext httpContext, IBeerService beerService) =>
            {
                var respose = beerService.GetAllBeers();
                
                if(respose.Any())
                    return Results.Ok(respose);
                
                return Results.NotFound();
                
            })
            .WithName("GetBeers")
            .WithOpenApi();

        app.Run();
    }
}