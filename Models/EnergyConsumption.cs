using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dashboard_Management.Models;

[Table("energy_consumption")]
public class EnergyConsumption
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("consumption_id")]
    public int ConsumptionId { get; set; }

    [Column("floor_id")]
    public int FloorId { get; set; }

    [Column("timestamp")]
    public DateTime Timestamp { get; set; }

    [Column("energy_consumed_kwh")]
    public decimal EnergyConsumedKwh { get; set; }

    [Column("peak_load_kw")]
    public decimal PeakLoadKw { get; set; }

    [Column("avg_temperature_c")]
    public decimal AvgTemperatureC { get; set; }

    [Column("co2_emissions_kg")]
    public decimal Co2EmissionsKg { get; set; }

    // Navigation property
    [ForeignKey(nameof(FloorId))]
    public Floor Floor { get; set; }
}
