using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class UpdateProfileDto {
	[Required(ErrorMessage = "Name is required"), MaxLength(20)]
	public string Name { get; set; }
	[MaxLength(15)]
	public string? Mobile { get; set; }
}