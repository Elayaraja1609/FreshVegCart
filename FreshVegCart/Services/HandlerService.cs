using Microsoft.AspNetCore.Components;
using Refit;

namespace FreshVegCart.Services;

public class HandlerService
{
	private readonly AppState _appState;

	public HandlerService(AppState appState) {
		_appState = appState;
	}

	public event Action<string>? NavigationRequested;

	public async Task CallApiAsync(Func<Task> apiCall) { 
		_appState.ShowLoader();
		try
		{
			await apiCall.Invoke();
		}
		catch (ApiException ex) {
			if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized) { 
				await MauiInterop.AlertAsync("Your session has expired, please login again.", "Unauthorized");
				NavigationRequested?.Invoke("/login");
				return;
			}
			await MauiInterop.AlertAsync(ex.Message, "Error");

		}
		catch (Exception ex)
		{
			await MauiInterop.AlertAsync(ex.Message, "Error");
		}
		finally
		{
			_appState.hideLoader();
		}
	}
}
