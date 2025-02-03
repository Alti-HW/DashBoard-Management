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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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



builder.Services.AddSwaggerGen();

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
var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Register custom exception middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
