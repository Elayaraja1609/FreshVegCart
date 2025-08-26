using CommunityToolkit.Maui.Alerts;
using System.Text.Json;

namespace FreshVegCart.Services;

public static class MauiInterop {
	private readonly static Page _page = App.Current.Windows[0].Page;
	public static async Task ShowToastAsync(string message) => 
		await Toast.Make(message).Show();
	public static async Task AlertAsync(string message,string title="Alert")=>
		await _page.DisplayAlert(title, message, "OK");

	public static async Task<bool> ConfirAsync(string message, string title = "Confirm") =>
		await _page.DisplayAlert(title, message, "Yes","No");

	public static void SaveToStorage<TValue>(string key, TValue value)
	{
		var serialized = JsonSerializer.Serialize(value);
		Preferences.Default.Set(key, serialized);
	}

	public static TValue GetFromStorage<TValue>(string key, TValue tvalue)
	{
		if (Preferences.Default.ContainsKey(key))
		{
			var seralizedvalue = Preferences.Default.Get<string?>(key, null);
			if (!string.IsNullOrWhiteSpace(seralizedvalue))
			{
				return JsonSerializer.Deserialize<TValue>(seralizedvalue)!;
			}
		}
		return tvalue;
	}
	public static void RemoveFromStorage(string key)
	{
		if (Preferences.Default.ContainsKey(key))
		{
			Preferences.Default.Remove(key);
		}
	}
}
