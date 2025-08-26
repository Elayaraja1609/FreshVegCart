namespace FreshVegCart.Shared.Dtos;

public record LoggedInUser(int Id, string Name, string Email,string? Mobile, string Token);