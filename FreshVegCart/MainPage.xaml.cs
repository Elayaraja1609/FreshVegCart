using FreshVegCart.Services;

namespace FreshVegCart
{
    public partial class MainPage : ContentPage
    {
        public MainPage(AppState appState)
        {
            InitializeComponent();
            BindingContext = appState;
		}
    }
}
