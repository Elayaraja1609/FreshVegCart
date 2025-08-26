using FreshVegCart.Apis;
using Microsoft.Extensions.Logging;
using Refit;
using CommunityToolkit.Maui;
using FreshVegCart.Services;
using FreshVegCart.Data;

namespace FreshVegCart
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();
            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<Services.CartService>()
                .AddSingleton<AppState>()
                .AddSingleton<AuthState>()
                .AddSingleton<CartDBServices>()
                .AddSingleton<HandlerService>();

            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            //const string baseApiUrl = "https://localhost:7168";
            const string baseApiUrl = "https://hqj8sz7x-7168.use.devtunnels.ms";
            services.AddRefitClient<IProductApi>().ConfigureHttpClient(SetHttpClient);
            services.AddRefitClient<IAuthApi>().ConfigureHttpClient(SetHttpClient);
            services.AddRefitClient<IOrderApi>(GetRefitSetting).ConfigureHttpClient(SetHttpClient);
            services.AddRefitClient<IUserApi>(GetRefitSetting).ConfigureHttpClient(SetHttpClient);
            static void SetHttpClient(HttpClient httpClient) => httpClient.BaseAddress = new Uri(baseApiUrl);
            static RefitSettings GetRefitSetting(IServiceProvider sp)
            {
                var authService = sp.GetRequiredService<AuthState>();
                var settings = new RefitSettings {
                    AuthorizationHeaderValueGetter = (_, __) => 
                    Task.FromResult(authService.IsLoggedIn ? authService.User!.Token : "")
                };
                return settings;
            }
        }
    }
}