using CommunityToolkit.Maui.Alerts;
using System.ComponentModel;

namespace FreshVegCart.Services;

public class AppState : INotifyPropertyChanged
{
	private bool _isBusy;
	public bool IsBusy
	{
		get => _isBusy;
		set
		{
			if (_isBusy != value)
			{
				_isBusy = value;
				PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsBusy)));
			}
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	public void ShowLoader()=>IsBusy = true;
	public void hideLoader() => IsBusy = false;

}
