using System.ComponentModel.DataAnnotations;

namespace FreshVegCart.Shared.Dtos;

public class AddressDto
{
	[Required]
	public string Address { get; set; }
	public int Id { get; set; }
	[Required]
	public string Name { get; set; }
	public bool IsDefault { get; set; }
}