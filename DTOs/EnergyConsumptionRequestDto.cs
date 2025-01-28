using System.ComponentModel.DataAnnotations;

namespace Dashboard_Management.DTOs
{
    public class EnergyConsumptionRequestDto
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
