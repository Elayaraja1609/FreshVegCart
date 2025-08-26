using FreshVegCart.API.Data;
using FreshVegCart.API.Data.Entities;
using FreshVegCart.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace FreshVegCart.API.Services;

public class AuthService {
	private readonly DataContext _context;
	private readonly IPasswordHasher<User> _passwordHasher;
	private readonly TokenService _tokenService;
	public AuthService(DataContext context, IPasswordHasher<User> passwordHasher, TokenService tokenService)
	{
		_passwordHasher = passwordHasher;
		_context = context;
		_tokenService = tokenService;
	}
	public async Task<ApiResult> RegisterAsync(RegisterDto dto) {
		if (await _context.Users.AnyAsync(u => u.Email == dto.Email)) {
			return ApiResult.Failure("Email already exists");
		}
		var user = new User
		{
			Email = dto.Email,
			Name = dto.Name,
			Mobile = dto.Mobile
		};
		user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
		try
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return ApiResult.Success();
		}
		catch (Exception ex) {
			return ApiResult.Failure(ex.Message);
		}
	}

	public async Task<ApiResult<LoggedInUser>> LoginAsync(LoginDto dto) {
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Username);
		if (user is null) return ApiResult<LoggedInUser>.Failure("User does not exist");

		var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash,dto.Password);
		if (verificationResult != PasswordVerificationResult.Success) {
			return ApiResult<LoggedInUser>.Failure("Incorrect password");
		}

		var jwt = _tokenService.GenerateJwtToken(user);
		var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email,user.Mobile, jwt);
		return ApiResult<LoggedInUser>.Success(loggedInUser);
	}
}
