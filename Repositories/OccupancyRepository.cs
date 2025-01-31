using Dashboard_Management.Data;
using Dashboard_Management.DTOs.Occupancy;
using Dashboard_Management.Interfaces.Occupancy;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_Management.Repositories
{
    public class OccupancyRepository : IOccupancyRepository
    {
        private readonly ApplicationDbContext _context;

        public OccupancyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BuildingOccupancyDto>> GetOccupancyDataAsync(OccupancyRequestDto request)
        {
            var query = _context.OccupancyData
                .Where(od =>
                    (!request.FloorId.HasValue || od.Floor.FloorId == request.FloorId) &&  // Filter by FloorId
                    (!request.BuildingId.HasValue || od.Floor.BuildingId == request.BuildingId) &&  // Filter by BuildingId
                    (request.EndDate != null
                        ? DateOnly.FromDateTime(od.Timestamp.Date) >= request.StartDate &&
                          DateOnly.FromDateTime(od.Timestamp.Date) <= request.EndDate
                        : DateOnly.FromDateTime(od.Timestamp.Date) == request.StartDate)) // Exact date match if only StartDate provided
                .GroupBy(od => new { od.Floor.BuildingId, od.Floor.Building.Name })  // Group by Building
                .Select(buildingGroup => new BuildingOccupancyDto
                {
                    BuildingId = buildingGroup.Key.BuildingId,
                    BuildingName = buildingGroup.Key.Name,
                    Floors = buildingGroup
                        .GroupBy(od => new { od.Floor.FloorId, od.Floor.FloorNumber }) // Group by Floor inside Building
                        .Select(floorGroup => new FloorOccupancyDto
                        {
                            FloorId = floorGroup.Key.FloorId,
                            FloorNumber = floorGroup.Key.FloorNumber,
                            TotalOccupancyCount = floorGroup.Sum(od => od.OccupancyCount),
                            AvgOccupancyRatio = Math.Round(floorGroup.Average(od => (decimal?)od.AvgOccupancyRatio) ?? 0, 2),
                            OccupancyDetails = floorGroup.Select(od => new OccupancyDetailDto
                            {
                                OccupancyId = od.OccupancyId,
                                FloorId = od.FloorId,
                                Timestamp = od.Timestamp,
                                OccupancyCount = od.OccupancyCount,
                                AvgOccupancyRatio = od.AvgOccupancyRatio
                            }).ToList()
                        }).ToList()
                });

            return await query.ToListAsync();
        }

    }

}
