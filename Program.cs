using Dashboard_Management.Data;
using Dashboard_Management.DTOs;
using Dashboard_Management.Helpers;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Interfaces.Occupancy;
using Dashboard_Management.Middlewares;
using Dashboard_Management.Repositories;
using Dashboard_Management.Services;
using Dashboard_Management.Validators.Energy;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEnergyService, EnergyService>();
builder.Services.AddScoped<IEnergyRepository, EnergyRepository>();
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
// Register Occupancy Service & Repository
builder.Services.AddScoped<IOccupancyRepository, OccupancyRepository>();
builder.Services.AddScoped<IOccupancyService, OccupancyService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Manually register individual validators
builder.Services.AddTransient<IValidator<EnergyConsumptionRequestDto>, EnergyConsumptionRequestValidator>();
builder.Services.AddTransient<IValidator<MetricRequestDto>, MetricRequestValidator>();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DashBoard Management API",
        Version = "v1",
        Description = "DashBoard Management API",
    });

    // Enable JWT Authentication in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT Bearer token in the format: Bearer {your-token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
            .AllowAnyHeader();
        });
});


var keycloakConfig = configuration.GetSection(KeyCloakConfiguration.Section).Get<KeyCloakConfiguration>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{keycloakConfig.ServerUrl}/realms/{keycloakConfig.Realm}";
        options.Audience = keycloakConfig.ClientId;
        options.RequireHttpsMetadata = false; // Set to true in production
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = keycloakConfig.ClientId
        };
    });

// Read the URL from appsettings.json
var kestrelSection = builder.Configuration.GetSection("Kestrel:Endpoints:Http:Url");
var url = kestrelSection.Value ?? "http://localhost:5000"; // Default if not set

builder.WebHost.UseUrls(url); // Correct way to set URL
var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Register custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
