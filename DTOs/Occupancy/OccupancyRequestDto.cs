using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Dashboard_Management.DTOs.Occupancy
{
    public class OccupancyRequestDto
    {
        [Required]
        [DefaultValue("2025-01-22")]
        public DateOnly? StartDate { get; set; }

        [DefaultValue(null)]
        public DateOnly? EndDate { get; set; }
        [DefaultValue(null)]
        public int? BuildingId { get; set; } // Optional filter for building
        [DefaultValue(null)]
        public int? FloorId { get; set; } // Optional filter for building
    }

}
