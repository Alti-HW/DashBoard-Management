using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard_Management.Data;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_Management.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly ApplicationDbContext _context;

        public BuildingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Building>> GetAllBuildingsWithFloorsAsync()
        {
            try
            {


                return await _context.Buildings
                    .Include(b => b.Floors)
                    .ToListAsync();
            }
            catch(Exception ex)
            {

            }
            return new List<Building>();
        }
    }
}
