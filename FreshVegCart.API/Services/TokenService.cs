using FreshVegCart.API.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FreshVegCart.API.Services
{
	public class TokenService
	{
		private readonly IConfiguration _configuration;
		public TokenService(IConfiguration configuration) {
			_configuration= configuration;
		}
		public string GenerateJwtToken(User user)
		{
			Claim[] claims = [
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Name),
			new Claim(ClaimTypes.Email, user.Email)
				];
			var expiration = _configuration.GetValue<int>("Jwt:ExpiryInMinutes");
			var secretKey = _configuration.GetValue<string>("Jwt:Secretkey");
			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: _configuration.GetValue<string>("Jwt:Issuer"),
				audience: _configuration.GetValue<string>("Jwt:Audience"),
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(expiration),
				signingCredentials: creds
			);
			var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
			return jwtToken;
		}
	}
}
