using System.ComponentModel.DataAnnotations;
namespace FreshVegCart.API.Data.Entities;

public class  Product
{
	[Key]
	public int Id { get; set; }
	[Required(ErrorMessage = "Name is required"), MaxLength(50)]
	public string Name { get; set; }
	[Required(ErrorMessage = "Description is required"), MaxLength(200)]
	public string ImageUrl { get; set; }
	public decimal Price { get; set; }
	[Required, MaxLength(10)]
	public string Unit { get; set; }

	public static Product[] GetSeedData()
	{
		//const string BaseImageUrl = "https://github.com/Elayaraja1609/Image-Icons/tree/main/Vegetables/";
		const string BaseImageUrl = "https://raw.githubusercontent.com/Elayaraja1609/Image-Icons/refs/heads/main/Vegetables/";

		Product[] products = [
			new () { Id = 1, Name = "Avocado", ImageUrl = $"{BaseImageUrl}avocado.png", Unit = "each", Price = 1.99m },
			new () { Id = 2, Name = "Beet", ImageUrl = $"{BaseImageUrl}beet.png", Unit = "each", Price = 0.99m },
			new () { Id = 3, Name = "Bell Pepper", ImageUrl = $"{BaseImageUrl}bell_pepper.png", Unit = "each", Price = 1.50m },
			new () { Id = 4, Name = "Broccoli", ImageUrl = $"{BaseImageUrl}broccoli.png", Unit = "each", Price = 2.00m },
			new () { Id = 5, Name = "Cabbage", ImageUrl = $"{BaseImageUrl}cabbage.png", Unit = "each", Price = 1.20m },
			new () { Id = 6, Name = "Capsicum", ImageUrl = $"{BaseImageUrl}capsicum.png", Unit = "each", Price = 1.80m },
			new () { Id = 7, Name = "Carrot", ImageUrl = $"{BaseImageUrl}carrot.png", Unit = "kg", Price = 0.80m },
			new () { Id = 8, Name = "Cauliflower", ImageUrl = $"{BaseImageUrl}cauliflower.png", Unit = "each", Price = 2.50m },
			new () { Id = 9, Name = "Coriander", ImageUrl = $"{BaseImageUrl}coriander.png", Unit = "bunch", Price = 0.70m },
			new () { Id = 10, Name = "Corn", ImageUrl = $"{BaseImageUrl}corn.png", Unit = "each", Price = 1.00m },
			new () { Id = 11, Name = "Cucumber", ImageUrl = $"{BaseImageUrl}cucumber.png", Unit = "each", Price = 0.90m },
			new () { Id = 12, Name = "Eggplant", ImageUrl = $"{BaseImageUrl}eggplant.png", Unit = "each", Price = 1.75m },
			new () { Id = 13, Name = "Farmer", ImageUrl = $"{BaseImageUrl}farmer.png", Unit = "each", Price = 5.00m },
			new () { Id = 14, Name = "Ginger", ImageUrl = $"{BaseImageUrl}ginger.png", Unit = "kg", Price = 2.20m },
			new () { Id = 15, Name = "Green Beans", ImageUrl = $"{BaseImageUrl}green_beans.png", Unit = "kg", Price = 1.60m },
			new () { Id = 16, Name = "Ladyfinger", ImageUrl = $"{BaseImageUrl}ladyfinger.png", Unit = "kg", Price = 1.10m },
			new () { Id = 17, Name = "Lettuce", ImageUrl = $"{BaseImageUrl}lettuce.png", Unit = "each", Price = 1.30m },
			new () { Id = 18, Name = "Mushroom", ImageUrl = $"{BaseImageUrl}mushroom.png", Unit = "kg", Price = 2.80m },
			new () { Id = 19, Name = "Onion", ImageUrl = $"{BaseImageUrl}onion.png", Unit = "kg", Price = 0.60m },
			new () { Id = 20, Name = "Pea", ImageUrl = $"{BaseImageUrl}pea.png", Unit = "kg", Price = 1.40m },
			new () { Id = 21, Name = "Potato", ImageUrl = $"{BaseImageUrl}potato.png", Unit = "kg", Price = 0.50m },
			new () { Id = 22, Name = "Pumpkin", ImageUrl = $"{BaseImageUrl}pumpkin.png", Unit = "each", Price = 3.00m },
			new () { Id = 23, Name = "Radish", ImageUrl = $"{BaseImageUrl}radish.png", Unit = "bunch", Price = 0.85m },
			new () { Id = 24, Name = "Red Chili", ImageUrl = $"{BaseImageUrl}red_chili.png", Unit = "kg", Price = 1.50m },
			new () { Id = 25, Name = "Spinach", ImageUrl = $"{BaseImageUrl}spinach.png", Unit = "bunch", Price = 1.20m },
			new () { Id = 26, Name = "Tomato", ImageUrl = $"{BaseImageUrl}tomato.png", Unit = "kg", Price = 0.95m },
			new () { Id = 27, Name = "Turnip", ImageUrl = $"{BaseImageUrl}turnip.png", Unit = "each", Price = 0.75m },
			new () { Id = 28, Name = "Vegetables", ImageUrl = $"{BaseImageUrl}vegetables.png", Unit = "each", Price = 4.00m },
			new () { Id = 29, Name = "Yellow Capsicum", ImageUrl = $"{BaseImageUrl}yellow_capsicum.png", Unit = "each", Price = 1.80m }
			];

		return products;
	}
}
