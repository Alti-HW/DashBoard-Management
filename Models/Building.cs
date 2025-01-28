using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard_Management.Models
{
    [Table("buildings")]
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("building_id")]
        public int BuildingId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("location")]
        public string Location { get; set; }

        [Column("total_floors")]
        public int TotalFloors { get; set; }

        [Column("area_sqft")]
        public int AreaSqft { get; set; }

        [Column("constructed_year")]
        public int ConstructedYear { get; set; }

        [Column("energy_goal_kwh")]
        public int EnergyGoalKwh { get; set; }

        [Column("peak_load_kw")]
        public int PeakLoadKw { get; set; }

        [Column("renewable_usage_pct")]
        public decimal RenewableUsagePct { get; set; }

        [Column("area_id")]
        public int AreaId { get; set; }

        [ForeignKey("AreaId")]
        public Area Area { get; set; }

        public ICollection<Floor> Floors { get; set; }
    }
}
