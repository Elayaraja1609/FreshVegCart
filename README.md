# FreshVegCart

FreshVegCart is a cross-platform .NET MAUI application for ordering fresh vegetables and fruits. It supports Android, iOS, Mac Catalyst, and Windows.

## Features

- Browse a variety of vegetables and fruits with images.
- Add items to your cart and manage quantities.
- Place orders and select delivery addresses.
- View order history and order details.
- Manage your profile and addresses.
- User authentication and registration.
- Change password and update profile.
- Responsive UI with bottom navigation and modern design.
- FAQ and Help section.

## Technologies Used

- [.NET MAUI](https://github.com/dotnet/maui) for cross-platform UI.
- ASP.NET Core Web API (FreshVegCart.API) for backend services.
- Entity Framework Core for data access.
- Refit for API calls.
- SQLite for local storage.
- CommunityToolkit.Maui for UI enhancements.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later with MAUI workload installed.

### Building the Project

1. Clone the repository.
2. Open `FreshVegCart.sln` in Visual Studio.
3. Restore NuGet packages.
4. Build and run the solution on your preferred platform (Android, iOS, Windows, Mac Catalyst).

### Project Structure

- `FreshVegCart/` - .NET MAUI client app.
- `FreshVegCart.API/` - ASP.NET Core Web API backend.
- `FreshVegCart.Shared/` - Shared DTOs and models.

### Configuration

- App icons and splash screens are in `Resources/`.
- Images for products are loaded from GitHub URLs.
- API endpoints require authentication (JWT).

## Contributing

Contributions are welcome! Please fork the repo and submit a pull request.

## License

This project is licensed under the MIT License.

## Credits

Designed and developed by [Elayaraja](https://www.youtube.com/@@elayarajavisu7718).