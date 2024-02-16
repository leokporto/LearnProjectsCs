using PizzaStore.Services;

namespace PizzaStore.Extensions
{
	public static class AuthRouteExtensions
	{
		public static WebApplication MapAuthRoutes(this WebApplication app)
		{
			app.MapGet("/auth", (TokenService service) => {
				var currentUser = new Models.User(1, "strauss1", "123", @"strauss@thedomain.com", new string[] { "Customer", "Premium" });
				
				return service.Create(currentUser);
			});
			return app;
		}
	}
}
