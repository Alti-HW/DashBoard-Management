using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard_Management.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("area_id")]
        public int AreaId { get; set; }

        [Required]
        [Column("city_id")]
        public int CityId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("population_density")]
        public decimal PopulationDensity { get; set; }

        [Column("avg_income")]
        public decimal AvgIncome { get; set; }

        [Column("crime_rate")]
        public decimal CrimeRate { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public ICollection<Building> Buildings { get; set; }
    }
}
