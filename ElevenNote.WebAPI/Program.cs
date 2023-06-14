using ElevenNote.Data;
using ElevenNote.Services.User;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ElevenNote.Services.Token;

var builder = WebApplication.CreateBuilder(args);
// Add connection string and DbContext setup
var connectionString=builder.Configuration.GetConnectionString("DefaultConnection");
 builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add User Service/Interface for Dependency Injection here
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""))
        };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adds AuthenticationMiddleware to the IApplicationBuilder
// This enables authentication capabilities
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

