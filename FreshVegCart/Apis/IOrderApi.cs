using FreshVegCart.Shared.Dtos;
using Refit;

namespace FreshVegCart.Apis;

[Headers("Authorization: Bearer ")]
public interface IOrderApi {
	[Post("/api/orders/place-order")]
	Task<ApiResult> PlaceOrderAsync(PlaceOrderDto dto);
	[Get("/api/orders/users/{userId}")]
	Task<OrderDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize);
	[Get("/api/orders/users/{userId}/orders/{orderId}/items")]
	Task<ApiResult<OrderDto>> GetUserOrderItemsAsync(int orderId, int userId);
}
