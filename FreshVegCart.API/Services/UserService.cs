using FreshVegCart.API.Data;
using FreshVegCart.API.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
namespace FreshVegCart.API.Services;

public class UserService
{
	private readonly DataContext _context;
	private readonly IPasswordHasher<User> _passwordHasher;
	private readonly TokenService _tokenService;

	public UserService(DataContext context, IPasswordHasher<User> passwordHasher, TokenService tokenService)
	{
		_context = context;
		_passwordHasher = passwordHasher;
		_tokenService = tokenService;
	}
	public async Task<ApiResult> SaveAddressAsync(AddressDto dto, int userId)
	{
		UserAddress? userAddress = null;
		if (dto.Id == 0)
		{
			var address = new UserAddress
			{
				AddressLine1 = dto.Address,
				City = "City",
				State = "State",
				Country = "Country",
				PostalCode = "000000",
				Id = dto.Id,
				IsDefault = dto.IsDefault,
				Name = dto.Name,
				UserId = userId
			};
			_context.UserAddresses.Update(address);

		}
		else {
			userAddress = await _context.UserAddresses.FindAsync(dto.Id);
			if (userAddress is null) {
				return ApiResult.Failure("Invalid request");
			}
			userAddress.AddressLine1 = dto.Address;
			userAddress.Name = dto.Name;
			userAddress.IsDefault = dto.IsDefault;
			userAddress.City = "City";
			userAddress.State = "State";
			userAddress.Country = "Country";
			userAddress.PostalCode = "000000";

			_context.UserAddresses.Update(userAddress);
		}
		try
		{
			if (dto.IsDefault)
			{
				var defaultAddress = await _context.UserAddresses
					.FirstOrDefaultAsync(a => a.UserId == userId && a.IsDefault && a.Id != dto.Id);
				if (defaultAddress is not null) {
					defaultAddress.IsDefault = false;
				}
			}
			await _context.SaveChangesAsync();
			return ApiResult.Success();
		}
		catch (Exception ex) {
			return ApiResult.Failure(ex.Message);
		}
	}

	public async Task<ApiResult<AddressDto[]>> GetAddressesAsync(int userId)
	{
		var addresses = await _context.UserAddresses.AsNoTracking()
			.Where(a => a.UserId == userId)
			.Select(a => new AddressDto
			{
				Id = a.Id,
				Name = a.Name,
				Address = a.AddressLine1,
				IsDefault = a.IsDefault
			}).ToArrayAsync();
		
		return ApiResult<AddressDto[]>.Success(addresses);
	}

	public async Task<ApiResult> DeleteAddressAsync(int addressId, int userId) {
		var address = await _context.UserAddresses.FindAsync(addressId);
		if (address is null || address.UserId != userId) {
			return ApiResult.Failure("Address does not exist");
		}
		try
		{
			_context.UserAddresses.Remove(address);
			await _context.SaveChangesAsync();
			return ApiResult.Success();
		}
		catch (Exception ex) {
			return ApiResult.Failure(ex.Message);
		}
	}
	public async Task<ApiResult> ChangePasswordAsync(ChangePasswordDto dto, int userId) {
		try {
			var user = await _context.Users.FindAsync(userId);

			if (user is null) return ApiResult.Failure("User does not exist");

			var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword);
			if (verificationResult != PasswordVerificationResult.Success) return ApiResult.Failure("Current Password is Incorrect");

			user.PasswordHash = _passwordHasher.HashPassword(user, dto.NewPassword);
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return ApiResult.Success();
		}
		catch(Exception ex)
		{
			return ApiResult.Failure(ex.Message);
		}
	}

	public async Task<ApiResult<LoggedInUser>> UpdateProfileAsync(UpdateProfileDto dto, int userId) {
		try {
			var user = await _context.Users.FindAsync(userId);
			if (user is null) return ApiResult<LoggedInUser>.Failure("User does not exist");
			user.Name = dto.Name;
			user.Mobile = dto.Mobile;
			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			var jwt = _tokenService.GenerateJwtToken(user);
			var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, user.Mobile, jwt);


			return ApiResult<LoggedInUser>.Success(loggedInUser);
		}
		catch (Exception ex) {
			return ApiResult<LoggedInUser>.Failure(ex.Message);
		}
	}
}
