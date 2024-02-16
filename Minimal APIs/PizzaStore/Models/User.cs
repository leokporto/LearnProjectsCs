namespace PizzaStore.Models
{
	public record User(
			int Id,
			string Username, 
			string Password,
			string Email,
			string[] Roles
		);
	
}
