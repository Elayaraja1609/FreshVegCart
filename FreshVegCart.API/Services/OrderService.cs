using FreshVegCart.API.Data;
using FreshVegCart.API.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
namespace FreshVegCart.API.Services;

public class OrderService 
{
	private readonly DataContext _context;

	public OrderService(DataContext context)
	{
		_context = context;
	}
	public async Task<ApiResult> PlaceOrderAsync(PlaceOrderDto dto, int userId) { 
		if(dto.Items.Length == 0) return ApiResult.Failure("Order must contain items");

		// ...existing code...
		var productIds = dto.Items.Select(i => i.ProductId).ToList();
		var products = await _context.Products
			.Where(p => productIds.Contains(p.Id))
			.ToDictionaryAsync(p => p.Id, p => p);
		// ...existing code...
		
		if(products.Count != dto.Items.Length) {
			return ApiResult.Failure("Invalid product details");
		}
		var orderItems = dto.Items.Select(i => new OrderItem
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				ProductImageUrl = products[i.ProductId].ImageUrl,
				ProductName = products[i.ProductId].Name,
				ProductPrice = products[i.ProductId].Price,
				Unit = products[i.ProductId].Unit
		}).ToList();
		var order = new Order
		{
			UserId = userId,
			UserAddressId = dto.UserAddressId,
			Address=dto.Address,
			AddressName = dto.AddressName,
			Items = orderItems,
			TotalItems = dto.Items.Length,
			TotalAmount = orderItems.Sum(i => i.ProductPrice * i.Quantity),
			Date = DateTime.UtcNow
		};

		try { 
		_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			return ApiResult.Success();
		}
		catch(Exception ex) { return ApiResult.Failure(ex.Message); }
	}
	public async Task<OrderDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize) =>
		 await _context.Orders.AsNoTracking()
			.Where(a => a.UserId == userId)
			.OrderByDescending(a=>a.Id)
			.Skip(startIndex).Take(pageSize)
			.Select(a=> new OrderDto { 
				Address = a.Address,
				AddressName = a.AddressName,
				Date = a.Date,
				Id = a.Id,
				Remarks = a.Remarks,
				Status = a.Status,
				TotalAmount = a.TotalAmount,
				TotalItems = a.TotalItems
			})
			.ToArrayAsync();
		
	public async Task<ApiResult<OrderDto>> GetUserOrderItemsAsync(int orderId, int userId) {
		
		var order = await _context.Orders.AsNoTracking()
			.Include(o=>o.Items)
			.FirstOrDefaultAsync(o => o.Id == orderId);
		if (order == null) return ApiResult<OrderDto>.Failure("Order not found");
		if(order.UserId != userId) ApiResult<OrderDto>.Failure("Order not found");

		var items= order.Items.Select(o => new OrderItemDto
		{
			Id = o.Id,
			ProductId = o.ProductId,
			ProductImageUrl = o.ProductImageUrl,
			ProductName = o.ProductName,
			ProductPrice = o.ProductPrice,
			Quantity = o.Quantity,
			Unit = o.Unit,
		}).ToArray();
		var itemsDto = new OrderDto
		{
			Id = order.Id,
			Address = order.Address,
			AddressName = order.AddressName,
			Date = order.Date,
			Remarks = order.Remarks,
			Status = order.Status,
			TotalAmount = order.TotalAmount,
			TotalItems = order.TotalItems,
			Items = items
		};
		return ApiResult<OrderDto>.Success(itemsDto);
	}
}