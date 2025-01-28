using System.Reflection.Metadata.Ecma335;
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

        public async Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(DateTime startDate, DateTime endDate)
        {
            try
            {

                var result = await _context.Buildings
               .Select(b => new BuildingEnergyConsumptionDto
               {
                   BuildingId = b.BuildingId,
                   BuildingName = b.Name,
                   TotalEnergyConsumedKwh = Math.Round(_context.EnergyConsumptions
                       .Where(ec => ec.Floor.BuildingId == b.BuildingId &&
                                    ec.Timestamp >= startDate && ec.Timestamp <= endDate)
                       .Sum(ec => (decimal?)ec.EnergyConsumedKwh) ?? 0, 2),  // Handle nulls

                   FloorConsumptions = _context.EnergyConsumptions
                       .Where(ec => ec.Floor.BuildingId == b.BuildingId &&
                                    ec.Timestamp >= startDate && ec.Timestamp <= endDate)
                       .GroupBy(ec => ec.Floor.FloorNumber)
                       .Select(g => new FloorEnergyConsumptionDto
                       {
                           FloorNumber = g.Key,
                           EnergyConsumedKwh = Math.Round(g.Sum(ec => (decimal?)ec.EnergyConsumedKwh) ?? 0, 2)
                       })
                       .ToList()
               })
               .ToListAsync();

                return result;

            }
            catch (Exception ex)
            {
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
                                .Where(ec => ec.FloorId == f.FloorId && ec.Timestamp >= request.StartDate && ec.Timestamp <= request.EndDate)
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
                                .Where(ec => ec.FloorId == f.FloorId && ec.Timestamp >= request.StartDate && ec.Timestamp <= request.EndDate)
                                .Sum(ec => (decimal?)ec.EnergyConsumedKwh) /
                            _context.OccupancyData
                                .Where(od => od.FloorId == f.FloorId && od.Timestamp >= request.StartDate && od.Timestamp <= request.EndDate)
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
