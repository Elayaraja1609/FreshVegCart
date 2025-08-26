using System.ComponentModel.DataAnnotations;
namespace FreshVegCart.API.Data.Entities;

public class UserAddress
{
	[Key]
	public int Id { get; set; }
	public int UserId { get; set; }
	public virtual User User { get; set; }
	[Required,MaxLength(20)]
	public string Name { get; set; }
	public bool IsDefault { get; set; }
	[Required(ErrorMessage = "Address is required"), MaxLength(200)]
	public string AddressLine1 { get; set; }
	[MaxLength(200)]
	public string? AddressLine2 { get; set; }
	[Required(ErrorMessage = "City is required"), MaxLength(50)]
	public string City { get; set; }
	[Required(ErrorMessage = "State is required"), MaxLength(50)]
	public string State { get; set; }
	[Required(ErrorMessage = "Country is required"), MaxLength(50)]
	public string Country { get; set; }
	[Required(ErrorMessage = "PostalCode is required"), MaxLength(20)]
	public string PostalCode { get; set; }
}