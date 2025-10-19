using WebShop.Infrastructure.Seeders;
using WebShopProject.Infrastructure.Extension;
using WebShopProject.Application.Extensions;
using WebShop.API.ErrorHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;
using WebShopProject.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDataSeeder, DataSeeder>(); // registracija

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy
            .WithOrigins(
                "http://localhost:3000",
                "http://localhost:3001",
                "https://localhost:3000",
                "https://localhost:3001"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var stripeInfo = builder.Configuration.GetSection("StripeSettings").Get<StripeInfo>();
StripeConfiguration.ApiKey = stripeInfo.SecretKey;

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Ensure the database is seeded
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.Seed();

    // Poruka u terminal
    Console.WriteLine("✅ Database has been seeded successfully!");
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

// Add CORS middleware here
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

