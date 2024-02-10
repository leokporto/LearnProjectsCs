public static class MiddlewareExtensions
{
    public static WebApplication UseSwaggerService(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pizzastore API v1"));

        return app;
    }
}
