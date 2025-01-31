
using Dashboard_Management.Models;
namespace Dashboard_Management.Interfaces
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<Building>> GetAllBuildingsWithFloorsAsync();
    }
}
