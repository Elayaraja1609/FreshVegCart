using System.ComponentModel.DataAnnotations;
namespace FreshVegCart.API.Data.Entities;

public class User
{
	[Key]
	public int Id { get; set; }
	[Required(ErrorMessage = "Name is required"),MaxLength(20)]
	public string Name { get; set; }
	[Required(ErrorMessage = "Email is required"),MaxLength(150)]
	public string Email { get; set; }
	[MaxLength(15)]
	public string Mobile { get; set; }
	[Required]
	public string PasswordHash { get; set; }
	public ICollection<UserAddress> Addresses { get; set; } = [];
}
