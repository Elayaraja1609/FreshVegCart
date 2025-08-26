using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class RegisterDto
{
	[Required]
	public string Name {  set; get; }
	[Required]
	public string Email { set; get; }
	public string Mobile { get; set; }
	[Required]
	public string Password { set; get; }
}
