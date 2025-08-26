using FreshVegCart.API.Services;
using FreshVegCart.Shared;
using FreshVegCart.Shared.Dtos;
using System.Security.Claims;

namespace FreshVegCart.API.Endpoints;

public static class UserEndpoints
{
	public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
	{
		var userGroup = app.MapGroup("/api/users")
			.RequireAuthorization()
			.WithTags("Users");
		userGroup.MapPost("/addresses", async (AddressDto dto, UserService service, ClaimsPrincipal principal) => { 
			return Results.Ok(await service.SaveAddressAsync(dto,principal.GetUserId()));
		})
			.Produces<ApiResult>()
			.WithName("Save-Address");

		userGroup.MapGet("/addresses", async (UserService service, ClaimsPrincipal principal) => {
			return Results.Ok(await service.GetAddressesAsync(principal.GetUserId()));
		})
			.Produces<AddressDto>()
			.WithName("Get-Address");

		userGroup.MapPost("/change-password", async (ChangePasswordDto dto, UserService service, ClaimsPrincipal principal) => {
			return Results.Ok(await service.ChangePasswordAsync(dto, principal.GetUserId()));
		})
			.Produces<ApiResult>()
			.WithName("Change-Password");

		userGroup.MapPatch("/update-profile",async(UpdateProfileDto dto, UserService service, ClaimsPrincipal principal) =>
		{
			return Results.Ok(await service.UpdateProfileAsync(dto, principal.GetUserId()));
		})
			.Produces<ApiResult<LoggedInUser>>()
			.WithName("Update-Profile");
		userGroup.MapDelete("/addresses/{addressId}", async (int addressId, UserService service, ClaimsPrincipal principal) =>
		{ 
			return Results.Ok(await service.DeleteAddressAsync(addressId, principal.GetUserId()));
		})
			.Produces<ApiResult>()
			.WithName("Delete-Address");
		return app;
	}
}