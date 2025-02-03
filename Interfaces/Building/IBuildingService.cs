using Dashboard_Management.DTOs.Building;
using Dashboard_Management.Models;

namespace Dashboard_Management.Interfaces
{
    public interface IBuildingService
    {
        Task<IEnumerable<BuildingMinimalDto>> GetAllBuildingsWithFloorsAsync();
    }
}
