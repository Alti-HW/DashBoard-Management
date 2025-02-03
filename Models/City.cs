using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard_Management.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("city_id")]
        public int CityId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("country")]
        public string Country { get; set; }

        [Column("population")]
        public int Population { get; set; }

        [Column("area_sqkm")]
        public decimal AreaSqkm { get; set; }

        public ICollection<Area> Areas { get; set; }
    }
}
