using FreshVegCart.Shared.Constants;
using System.ComponentModel.DataAnnotations;
namespace FreshVegCart.API.Data.Entities;

public class Order {
	[Key]
	public int Id { get; set; }
	[Required(ErrorMessage = "UserId is required")]
	public int UserId { get; set; }
	public virtual User User { get; set; }
	public DateTime Date { get; set; }
	public decimal TotalAmount { get; set; }
	[MaxLength(200)]
	public string? Remarks { get; set; }
	[Required,MaxLength(15)]
	public string Status { get; set; } = nameof(OrderStatus.Placed);
	public int UserAddressId { get; set; }
	public string Address { get; set; }
	public string AddressName { get; set; }

	public int TotalItems { get; set; }

	public virtual ICollection<OrderItem> Items { get; set; } = [];
}
