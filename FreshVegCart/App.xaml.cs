using FreshVegCart.Data;
using FreshVegCart.Services;

namespace FreshVegCart
{
    public partial class App : Application
    {
        private readonly AppState _state;
		private readonly CartDBServices _cartDBServices;
		private readonly CartService _cartService;

		public App( AppState appState, CartDBServices cartDBServices, CartService cartService)
        {
            InitializeComponent();
            _state = appState;
			_cartDBServices = cartDBServices;
			_cartService = cartService;
		}
		protected override async void OnStart()
		{
			base.OnStart();
			await _cartDBServices.CreateTable();
			await _cartService.InitializeCartAsync();
		}
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage(_state)) { Title = "Fresh Veg Cart" };
        }
    }
}
