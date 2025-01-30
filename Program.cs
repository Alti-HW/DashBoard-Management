using System.Security.Claims;
using Dashboard_Management.Data;
using Dashboard_Management.Extensions;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Middlewares;
using Dashboard_Management.Repositories;
using Dashboard_Management.Services;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<IEnergyService, EnergyService>();
services.AddScoped<IEnergyRepository, EnergyRepository>();


services.AddSwagger(configuration);

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // URL of your Keycloak or Identity Provider
                options.Authority = "http://localhost:8080/realms/Alti-EMS"; // URL of your Keycloak or Identity Provider
                options.Audience = "realm-management"; // Audience you expect from the token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RoleClaimType = "realm_access",
                    ValidateIssuerSigningKey = true,
                    // Additional settings for issuer validation can be added here if necessary
                };
            });

services.AddAuthorization(options =>
{
    options.AddPolicy("DashboardAdminPolicy", policy =>
        policy.RequireClaim("realm_access", "Dashboard-Admin"));
});

services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId("EMS");
    });
}

// Register custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();

