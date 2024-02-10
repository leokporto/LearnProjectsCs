using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
public static class PizzaRouteExtensions
{
    public static WebApplication MapPizzaRoutes(this WebApplication app)
    {
        app.MapGet("/pizzas", async (PizzaDb  db) => await db.Pizzas.ToListAsync());
        
        app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) => await db.Pizzas.FindAsync(id));
        
        app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) => 
        {
            await db.Pizzas.AddAsync(pizza);
            await db.SaveChangesAsync();
            return Results.Created($"/pizzas/{pizza.Id}", pizza);
        });

        app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) =>
        {
            var pizza = await db.Pizzas.FindAsync(id);
            if (pizza is null) return Results.NotFound();
            pizza.Name = updatepizza.Name;
            pizza.Description = updatepizza.Description;
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
        {
            var pizza = await db.Pizzas.FindAsync(id);
            if (pizza is null)
            {
                return Results.NotFound();
            }
            db.Pizzas.Remove(pizza);
            await db.SaveChangesAsync();
            return Results.Ok();
        });
        return app;
    }    
} 