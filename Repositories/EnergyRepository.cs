using System.Reflection.Metadata.Ecma335;
using Azure.Core;
using Dashboard_Management.Data;
using Dashboard_Management.DTOs;
using Dashboard_Management.Extensions;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Dashboard_Management.Repositories
{
    public class EnergyRepository : IEnergyRepository
    {
        private readonly ApplicationDbContext _context;

        public EnergyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(EnergyConsumptionRequestDto request)
        {
            try
            {
                var query =
                        from ec in _context.EnergyConsumptions
                        join b in _context.Buildings

                        //from b in _context.Buildings
                        //join ec in _context.EnergyConsumptions
                        //on b.BuildingId equals ec.Floor.BuildingId
                        on ec.Floor.BuildingId equals b.BuildingId
                        where (request.BuildingId.HasValue && b.BuildingId == request.BuildingId) &&
                                  (request.FloorId.HasValue && ec.Floor.FloorId == request.FloorId) &&
                                  (request.EndDate != null
                                        ? ec.Timestamp >= request.StartDate.StartOfDay() &&
                                          ec.Timestamp <= request.EndDate.EndOfDay()
                                        : ec.Timestamp >= request.StartDate.StartOfDay() &&
                                          ec.Timestamp <= request.StartDate.EndOfDay())
                        select new
                        {
                            BuildingId = b.BuildingId,
                            BuildingName = b.Name,
                            FloorId = ec.FloorId,
                            FloorNumber = ec.Floor.FloorNumber,
                            EnergyConsumedKwh = ec.EnergyConsumedKwh,
                            ConsumptionId = ec.ConsumptionId,
                            Timestamp = ec.Timestamp,
                            PeakLoadKw = ec.PeakLoadKw,
                            AvgTemperatureC = ec.AvgTemperatureC,
                            Co2EmissionsKg = ec.Co2EmissionsKg,
                        };

                var buildings = await query.ToListAsync();

                var floorConsumptionsLookup = buildings
                    .GroupBy(b => new { b.FloorId, b.FloorNumber, b.BuildingId })
                    .ToDictionary(g => g.Key, g => new
                    {
                        FloorId = g.Key.FloorId,
                        FloorNumber = g.Key.FloorNumber,
                        BuildingId = g.Key.BuildingId,
                        EnergyConsumedKwh = Math.Round(g.Sum(ec => (decimal?)ec.EnergyConsumedKwh) ?? 0, 2),
                        FloorDetails = g.Select(b => new EnergyConsumptionDetailDto
                        {
                            ConsumptionId = b.ConsumptionId,
                            FloorId = b.FloorId,
                            Timestamp = b.Timestamp,
                            EnergyConsumedKwh = b.EnergyConsumedKwh,
                            PeakLoadKw = b.PeakLoadKw,
                            AvgTemperatureC = b.AvgTemperatureC,
                            Co2EmissionsKg = b.Co2EmissionsKg,
                            CostPerUnit = 23,
                            TotalCost = b.EnergyConsumedKwh * 23
                        }).ToList()
                    });

                var result = buildings
                    .GroupBy(b => new { b.BuildingId, b.BuildingName })
                    .Select(b => new BuildingEnergyConsumptionDto
                    {
                        BuildingId = b.Key.BuildingId,
                        BuildingName = b.Key.BuildingName,
                        TotalEnergyConsumedKwh = Math.Round(b.Sum(c => (decimal?)c.EnergyConsumedKwh ?? 0), 2),
                        FloorConsumptions = b.GroupBy(f => new { f.FloorId, f.FloorNumber })
                            .Select(f =>
                            {
                                var key = new { f.Key.FloorId, f.Key.FloorNumber, b.Key.BuildingId };
                                var floorData = floorConsumptionsLookup.GetValueOrDefault(key);

                                return new FloorEnergyConsumptionDto
                                {
                                    FloorId = f.Key.FloorId,
                                    FloorNumber = f.Key.FloorNumber,
                                    EnergyConsumedKwh = floorData?.EnergyConsumedKwh ?? 0,
                                    FloorDetails = floorData?.FloorDetails ?? new List<EnergyConsumptionDetailDto>()
                                };
                            })
                            .ToList(),
                    })
                    .ToList();

                return result;

            }
            catch (Exception ex)
            {
                // Log the error (if logging is enabled)
            }

            return new List<BuildingEnergyConsumptionDto>();
        }

        public async Task<List<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request)
        {
            var query = _context.Floors.AsQueryable();

            // Optional building filter
            if (request.BuildingId.HasValue)
            {
                query = query.Where(f => f.BuildingId == request.BuildingId.Value);
            }

            if (request.MetricType == "persft")
            {
                return await query
                    .Select(f => new EnergyMetricResponseDto
                    {
                        BuildingId = f.BuildingId,
                        FloorId = f.FloorId,
                        FloorNumber = f.FloorNumber,
                        MetricType = "persft",
                        MetricValue = Math.Round(
                            _context.EnergyConsumptions
                                .Where(ec => ec.FloorId == f.FloorId && (request.EndDate != null
                                                 ? DateOnly.FromDateTime(ec.Timestamp) >= request.StartDate && DateOnly.FromDateTime(ec.Timestamp) <= request.EndDate
                                                 : DateOnly.FromDateTime(ec.Timestamp) == request.StartDate))
                                .Sum(ec => (decimal?)ec.EnergyConsumedKwh) / f.FloorAreaSqft ?? 0,
                            2
                        )
                    })
                    .ToListAsync();
            }
            else if (request.MetricType == "peroccupancy")
            {
                return await query
                    .Select(f => new EnergyMetricResponseDto
                    {
                        BuildingId = f.BuildingId,
                        FloorId = f.FloorId,
                        FloorNumber = f.FloorNumber,
                        MetricType = "peroccupancy",
                        MetricValue = Math.Round(
                            _context.EnergyConsumptions
                                .Where(ec => ec.FloorId == f.FloorId && (request.EndDate != null
                                                 ? DateOnly.FromDateTime(ec.Timestamp) >= request.StartDate && DateOnly.FromDateTime(ec.Timestamp) <= request.EndDate
                                                 : DateOnly.FromDateTime(ec.Timestamp) == request.StartDate))
                                .Sum(ec => (decimal?)ec.EnergyConsumedKwh) /
                            _context.OccupancyData
                                .Where(od => od.FloorId == f.FloorId && (request.EndDate != null
                                                 ? DateOnly.FromDateTime(od.Timestamp) >= request.StartDate && DateOnly.FromDateTime(od.Timestamp) <= request.EndDate
                                                 : DateOnly.FromDateTime(od.Timestamp) == request.StartDate))
                                .Sum(od => (decimal?)od.OccupancyCount) ?? 0,
                            2
                        )
                    })
                    .ToListAsync();
            }

            return new List<EnergyMetricResponseDto>();
        }

    }

}
