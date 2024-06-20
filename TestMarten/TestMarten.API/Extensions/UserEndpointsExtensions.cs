using Marten;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestMarten.API.Extensions
{
	public static class UserEndpointsExtensions
	{
		public static WebApplication MapUserEndpoints(this WebApplication app) 
		{
			// You can inject the IDocumentStore and open sessions yourself
			app.MapPost("/user",
				async (CreateUserRequest create, [FromServices] IDocumentStore store) =>
				{
					// Open a session for querying, loading, and updating documents
					await using var session = store.LightweightSession();

					var user = new User
					{
						FirstName = create.FirstName,
						LastName = create.LastName,
						Internal = create.Internal
					};
					session.Store(user);

					await session.SaveChangesAsync();
				});

			app.MapGet("/users",
				async (bool internalOnly, [FromServices] IDocumentStore store, CancellationToken ct) =>
				{
					// Open a session for querying documents only
					await using var session = store.QuerySession();

					return await session.Query<User>()
						.Where(x => x.Internal == internalOnly)
						.ToListAsync(ct);
				});

			// OR Inject the session directly to skip the management of the session lifetime
			app.MapGet("/user/{id:guid}",
				async (Guid id, [FromServices] IQuerySession session, CancellationToken ct) =>
				{
					return await session.LoadAsync<User>(id, ct);
				});

			return app;
		}
	}
}
