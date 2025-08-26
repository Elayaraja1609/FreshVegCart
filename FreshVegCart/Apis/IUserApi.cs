using FreshVegCart.Shared.Dtos;
using Refit;

namespace FreshVegCart.Apis;

[Headers("Authorization: Bearer ")]
public interface IUserApi
{
	[Post("/api/users/addresses")]
	Task<ApiResult> SaveAddressAsync(AddressDto dto);
	[Get("/api/users/addresses")]
	Task<ApiResult<AddressDto[]>> GetAddressesAsync();
	[Post("/api/users/change-password")]
	Task<ApiResult> ChangePasswordAsync(ChangePasswordDto dto);

	[Patch("/api/users/update-profile")]
	Task<ApiResult<LoggedInUser>> UpdateProfileAsync(UpdateProfileDto dto);
	[Delete("/api/users/addresses/{addressId}")]
	Task<ApiResult> DeleteAddressAsync(int addressId);
}