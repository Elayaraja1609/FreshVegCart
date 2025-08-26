using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class LoginDto
{
	[Required]
	public string Username { set; get; }
	[Required]
	public string Password { set; get; }
}
