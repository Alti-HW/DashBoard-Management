using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard_Management.DTOs;
using Dashboard_Management.DTOs.Building;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Repositories;

namespace Dashboard_Management.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<IEnumerable<BuildingMinimalDto>> GetAllBuildingsWithFloorsAsync()
        {
            var buildings = await _buildingRepository.GetAllBuildingsWithFloorsAsync();

            return buildings.Select(b => new BuildingMinimalDto
            {
                BuildingId = b.BuildingId,
                Name = b.Name,
                Floors = b.Floors?.Select(f => new FloorMinimalDto
                {
                    FloorId = f.FloorId,
                    FloorNumber = f.FloorNumber
                }).ToList()
            }).ToList();
        }
    }
}
