using FreshVegCart.API.Services;
using FreshVegCart.Shared.Dtos;

namespace FreshVegCart.API.Endpoints;

public static class ProductEndpoints
{
	public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app) {
		app.MapGet("/api/products", async (ProductService service)
			=> Results.Ok(await service.GetAllProductsAsync())).Produces<ProductDto>()
			.WithName("Products");
		return app;
	}
}
