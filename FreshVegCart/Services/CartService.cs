using FreshVegCart.Data;
using FreshVegCart.Shared.Dtos;

namespace FreshVegCart.Services;

public class CartService
{
	private readonly CartDBServices _cartDBServices;

	public CartService(CartDBServices cartDBServices)
	{
		_cartDBServices = cartDBServices;
	}
	public List<CartModel> Items { get; private set; } = [];

	public async Task InitializeCartAsync() {
		Items = await _cartDBServices.GetItemsAsync();
		NotifyCountChanged();
	}
	public int Count { get; private set; }
	public string CountDispaly => Count < 100 ? $"{Count}" : "99+";
	public event Action? CartCountChange;
	public decimal TotalPrice => Items.Sum(i => i.Amount);
	public async Task IncreaseQuantityAsync(ProductDto product)
	{
		var cartItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
		if(cartItem is null) { 
			cartItem = CartModel.FromDto(product);
			await _cartDBServices.AddItemsAsync(cartItem); 
			Items.Add(cartItem);
			
		}
		else
		{
			cartItem.Quantity=product.Quantity;
			await _cartDBServices.UpdateItemsAsync(cartItem);
		}

		NotifyCountChanged();
	}
	public async Task DecreaseQuantityAsync(ProductDto product)
	{
		var cartItem = Items.FirstOrDefault(i => i.ProductId == product.Id);
		if (cartItem is null)
		{
			return;
		}
		else
		{
			cartItem.Quantity = product.Quantity;
			if (cartItem.Quantity == 0)
			{
				Items.Remove(cartItem);
				await _cartDBServices.DeleteItemsAsync(cartItem.Id);
			}
			else { 
				await _cartDBServices.UpdateItemsAsync(cartItem);
			}
		}
		NotifyCountChanged();
	}
	public async Task IncreaseCartItemQuantityAsync(CartModel cartItem)
	{
		cartItem.Quantity++;
		await _cartDBServices.UpdateItemsAsync(cartItem);
		NotifyCountChanged();
	}
	public async Task DecreaseCartItemQuantityAsync(CartModel cartItem)
	{
		cartItem.Quantity--;
		if (cartItem.Quantity == 0)
		{
			Items.Remove(cartItem);
			await _cartDBServices.DeleteItemsAsync(cartItem.Id);

		}
		NotifyCountChanged();
	}
	public async Task RemoveItemAsync(CartModel cartItem)
	{
		Items.Remove(cartItem);
		NotifyCountChanged();
		await _cartDBServices.DeleteItemsAsync(cartItem.Id);

		await MauiInterop.ShowToastAsync("Cart Cleared.");

	}
	public async Task ClearCartAsync()
	{
		var app = App.Current;
		if (app == null || app.Windows == null || app.Windows.Count == 0 || app.Windows[0]?.Page == null)
			return;

		if (await MauiInterop.ConfirAsync("Are you sure, you want to clear the cart?", "Confirm"))
		{
			Items.Clear();
			await _cartDBServices.ClearCartAsync();
			NotifyCountChanged();
			await MauiInterop.ShowToastAsync("Cart cleared successfully.");
		}
	}
	public async Task ClearCartAfterPlaceOrderAsync() {
		Items.Clear();
		await _cartDBServices.ClearCartAsync();
		NotifyCountChanged();
	}
	private void NotifyCountChanged() { 
		Count = Items.Sum(i => i.Quantity);
		CartCountChange?.Invoke();
	}
}
