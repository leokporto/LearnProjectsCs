using Microsoft.IdentityModel.Tokens;
using PizzaStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaStore.Services
{
	public class TokenService
	{
		public string Create(User user) 
		{ 
			var handler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes("u@W2Gf$qr4aDP7WY%fgau@W2Gf$qr4aDP7WY%fga");

			var credentials = new SigningCredentials(
								new SymmetricSecurityKey(key),
								SecurityAlgorithms.HmacSha256);

			var descriptor = new SecurityTokenDescriptor()
			{
				SigningCredentials = credentials,
				Expires = DateTime.UtcNow.AddHours(1),
				Subject = GenerateClaims(user)
			};


			var token = handler.CreateToken(descriptor);

			return handler.WriteToken(token);
		}

		private ClaimsIdentity GenerateClaims(User user) { 
			var claims = new ClaimsIdentity(new Claim[] {
				new Claim("id", user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Email, user.Email)
			});

			foreach (var role in user.Roles) { 
				claims.AddClaim(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}
	}
}
