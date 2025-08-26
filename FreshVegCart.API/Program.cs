using FreshVegCart.API.Data;
using FreshVegCart.API.Data.Entities;
using FreshVegCart.API.Endpoints;
using FreshVegCart.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<AuthService>()
	.AddTransient<OrderService>()
	.AddTransient<ProductService>()
	.AddTransient<UserService>()
	.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>()
	.AddTransient<TokenService>();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	var issuer = builder.Configuration.GetValue<string>("Jwt:Issuer");
	var audience = builder.Configuration.GetValue<string>("Jwt:Audience");
	var secretKey = builder.Configuration.GetValue<string>("Jwt:Secretkey");
	var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = issuer,
		ValidateIssuer = true,
		IssuerSigningKey = key,
		ValidateIssuerSigningKey = true,
		ValidAudience = audience,
		ValidateAudience = true,
	};
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	AutoMigrateDb(app.Services);
}

app.UseHttpsRedirection();
app.UseAuthentication()
	.UseAuthorization();
app.MapAuthEndpoints()
	.MapOrderEndpoints()
	.MapProductEndpoints()
	.MapUserEndpoints();



app.Run();

static void AutoMigrateDb(IServiceProvider sp) { 
	using var scope = sp.CreateScope();
	var context = scope.ServiceProvider.GetRequiredService<DataContext>();
	if(context.Database.GetAppliedMigrations().Any())
		context.Database.Migrate();
}