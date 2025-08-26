using FreshVegCart.Shared.Constants;
using System.Text.Json.Serialization;

namespace FreshVegCart.Shared.Dtos;

public class OrderDto {
	public int Id { get; set; }
	public DateTime Date { get; set; }
	public decimal TotalAmount { get; set; }
	public string? Remarks { get; set; }
	public string Status { get; set; }
	public int UserAddressId { get; set; }
	public string Address { get; set; }
	public string AddressName { get; set; }
	public int TotalItems { get; set; }
	public OrderItemDto[] Items { get; set; } = [];
	[JsonIgnore]
	public string StatusColorClass => 
		Status switch
		{
			nameof(OrderStatus.Placed)=> "text-primary",
			nameof(OrderStatus.Delivered) => "text-success",
			nameof(OrderStatus.Accepted) => "text-warning",
			nameof(OrderStatus.Rejected) => "text-danger",
			_ => "text-secondary"
		};
}