using Dashboard_Management.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("floors")]
public class Floor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("floor_id")]
    public int FloorId { get; set; }

    [Column("building_id")]
    public int BuildingId { get; set; }

    [Column("floor_number")]
    public int FloorNumber { get; set; }

    [Column("floor_area_sqft")]
    public int FloorAreaSqft { get; set; }

    [Column("avg_occupancy")]
    public int AvgOccupancy { get; set; }

    [Column("hvac_power_kw")]
    public decimal HvacPowerKw { get; set; }

    [Column("lighting_power_kw")]
    public decimal LightingPowerKw { get; set; }

    [Column("equipment_power_kw")]
    public decimal EquipmentPowerKw { get; set; }

    [ForeignKey(nameof(BuildingId))]
    public required Building Building { get; set; }

    public ICollection<EnergyConsumption>? EnergyConsumptions { get; set; }
}
