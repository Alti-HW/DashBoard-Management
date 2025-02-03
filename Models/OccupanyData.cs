using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard_Management.Models
{
    [Table("occupancy_data")]
    public class OccupancyData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("occupancy_id")]
        public int OccupancyId { get; set; } // Primary key with auto-increment (SERIAL)

        [Required]
        [Column("floor_id")]
        public int FloorId { get; set; } // Foreign key to Floors table

        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; } // Non-null timestamp

        [Required]
        [Column("occupancy_count")]
        public int OccupancyCount { get; set; } // Non-null integer value

        [Required]
        [Column("avg_occupancy_ratio", TypeName = "decimal(5,2)")]
        public decimal AvgOccupancyRatio { get; set; } // Non-null decimal with precision (5,2)

        // Navigation property for the relationship
        [ForeignKey(nameof(FloorId))]
        public Floor Floor { get; set; }
    }
}
