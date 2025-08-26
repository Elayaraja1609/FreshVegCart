using SQLite;

namespace FreshVegCart.Data;

public class CartDBServices: IAsyncDisposable
{
	private const string DatabaseName = "FreshVegCart.db3";
	private readonly SQLiteAsyncConnection _databaseConnection;
	public CartDBServices()
	{
		var dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
		_databaseConnection = new SQLiteAsyncConnection(dbPath,
			SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
	}
	public async Task CreateTable() =>
		await _databaseConnection.CreateTableAsync<CartModel>();
	public async Task<List<CartModel>> GetItemsAsync() =>
		await _databaseConnection.Table<CartModel>().ToListAsync();
	public async Task AddItemsAsync(CartModel model) =>
		await _databaseConnection.InsertAsync(model);
	public async Task UpdateItemsAsync(CartModel model) =>
		await _databaseConnection.UpdateAsync(model);
	public async Task DeleteItemsAsync(int itemId) =>
		await _databaseConnection.DeleteAsync<CartModel>(itemId);
	public async Task ClearCartAsync() =>
		await _databaseConnection.DeleteAllAsync<CartModel>();

	public async ValueTask DisposeAsync()
	{
		if (_databaseConnection is not null) { 
			await _databaseConnection.CloseAsync();
		}
	}
}
