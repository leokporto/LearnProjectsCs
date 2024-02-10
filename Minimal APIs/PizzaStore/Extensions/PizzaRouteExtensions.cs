using Microsoft.EntityFrameworkCore;
using PizzaStore.Data;
using PizzaStore.Models;
public static class PizzaRouteExtensions
{
    public static WebApplication MapPizzaRoutes(this WebApplication app)
    {
        app.MapGet("/pizzas", async (PizzaDb  db) => 
        { 
            var allPizzas = await db.Pizzas.ToListAsync();

            return (allPizzas == null || allPizzas.Count == 0) ? Results.NotFound() : TypedResults.Ok(allPizzas);                
        });        
        
        app.MapGet("/pizzas/{id}", async (PizzaDb db, int id) =>
        { 
            var pizza = await db.Pizzas.FindAsync(id);

            return pizza is null ? Results.NotFound() : TypedResults.Ok(pizza);
        });
        
        app.MapPost("/pizzas", async (PizzaDb db, Pizza pizza) => 
        {
            if(pizza == null)
                return Results.BadRequest();

            try
            {
                await db.Pizzas.AddAsync(pizza);
                await db.SaveChangesAsync();
                return TypedResults.Created($"/pizzas/{pizza.Id}", pizza);
            }
            catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }             
        });

        app.MapPut("/pizza/{id}", async (PizzaDb db, Pizza updatepizza, int id) =>
        {
            if(id < 0)
                return Results.NotFound();

            try{
                var pizza = await db.Pizzas.FindAsync(id);
                if (pizza is null) 
                    return Results.NotFound();
                pizza.Name = updatepizza.Name;
                pizza.Description = updatepizza.Description;
                await db.SaveChangesAsync();
                
                return Results.NoContent();
            }
            catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }            
        });

        app.MapDelete("/pizza/{id}", async (PizzaDb db, int id) =>
        {
            if(id <0)
                return Results.BadRequest();

            try
            {
                var pizza = await db.Pizzas.FindAsync(id);
                if (pizza is null)
                {
                    return Results.BadRequest();
                }
                db.Pizzas.Remove(pizza);
                await db.SaveChangesAsync();

                return Results.Ok();
            }
            catch(Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            
        });
        return app;
    }    
} 