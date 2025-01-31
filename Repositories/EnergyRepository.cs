using System.Reflection.Metadata.Ecma335;
using Azure.Core;
using Dashboard_Management.Data;
using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces;
using Microsoft.EntityFrameworkCore;
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
                var query = _context.Buildings
                    .Where(b => !request.BuildingId.HasValue || b.BuildingId == request.BuildingId) // Apply BuildingId filter if given
                    .Select(b => new BuildingEnergyConsumptionDto
                    {
                        BuildingId = b.BuildingId,
                        BuildingName = b.Name,

                        // TotalEnergyConsumedKwh calculation
                        TotalEnergyConsumedKwh = Math.Round(
                            _context.EnergyConsumptions
                                .Where(ec => ec.Floor.BuildingId == b.BuildingId &&
                                             // Apply FloorId filter if provided
                                             (!request.FloorId.HasValue || ec.Floor.FloorId == request.FloorId) &&
                                             // Apply exact date match when only StartDate is provided
                                             (request.EndDate != null
                                                 ? DateOnly.FromDateTime(ec.Timestamp.Date) >= request.StartDate &&
                                                   DateOnly.FromDateTime(ec.Timestamp.Date) <= request.EndDate
                                                 : DateOnly.FromDateTime(ec.Timestamp.Date) == request.StartDate) // Exact match for StartDate
                                )
                                .Sum(ec => (decimal?)ec.EnergyConsumedKwh) ?? 0, 2),  // Handle nulls

                        // FloorConsumptions calculation
                        FloorConsumptions = request.BuildingId.HasValue // Only calculate floor consumptions if BuildingId is provided
                            ? _context.EnergyConsumptions
                                .Where(ec => ec.Floor.BuildingId == b.BuildingId &&
                                             // Apply FloorId filter if provided
                                             (!request.FloorId.HasValue || ec.Floor.FloorId == request.FloorId) &&
                                             // Apply exact date match when only StartDate is provided
                                             (request.EndDate != null
                                                 ? DateOnly.FromDateTime(ec.Timestamp.Date) >= request.StartDate &&
                                                   DateOnly.FromDateTime(ec.Timestamp.Date) <= request.EndDate
                                                 : DateOnly.FromDateTime(ec.Timestamp.Date) == request.StartDate))  // Exact match for StartDate
                                .GroupBy(ec => new { ec.Floor.FloorId, ec.Floor.FloorNumber }) // Group by FloorId & FloorNumber
                                .Select(g => new FloorEnergyConsumptionDto
                                {
                                    FloorId = g.Key.FloorId, // Include FloorId in response
                                    FloorNumber = g.Key.FloorNumber,
                                    EnergyConsumedKwh = Math.Round(g.Sum(ec => (decimal?)ec.EnergyConsumedKwh) ?? 0, 2),
                                    // Floor details list
                                    FloorDetails = g.Select(ec => new EnergyConsumptionDetailDto
                                    {
                                        ConsumptionId = ec.ConsumptionId,
                                        FloorId = ec.FloorId,
                                        Timestamp = ec.Timestamp,
                                        EnergyConsumedKwh = ec.EnergyConsumedKwh,
                                        PeakLoadKw = ec.PeakLoadKw,
                                        AvgTemperatureC = ec.AvgTemperatureC,
                                        Co2EmissionsKg = ec.Co2EmissionsKg,
                                        CostPerUnit = 23,
                                        TotalCost = ec.EnergyConsumedKwh * 23
                                    }).ToList()
                                })
                                .ToList()
                            : new List<FloorEnergyConsumptionDto>() // Empty if no BuildingId
                    });

                return await query.ToListAsync();
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
