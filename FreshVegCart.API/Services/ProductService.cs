using FreshVegCart.API.Data;
using FreshVegCart.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
namespace FreshVegCart.API.Services;

public class ProductService
{
	private readonly DataContext _context;
	
	public ProductService(DataContext context)
	{
		_context = context;
	}
	public async Task<ProductDto[]> GetAllProductsAsync() =>
		await _context.Products.AsNoTracking()
		.Select(p => new ProductDto {
			Id= p.Id,
			Name = p.Name,
			Price=p.Price,
			ImageUrl=p.ImageUrl,
			Unit=p.Unit
		}).ToArrayAsync();
}
