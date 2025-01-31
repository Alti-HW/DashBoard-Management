using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Dashboard_Management.DTOs
{
    public class MetricRequestDto
    {
        [Required]
        [DefaultValue("2024-01-20")]
        public DateOnly StartDate { get; set; }
        //[Required]
        [DefaultValue(null)]
        public DateOnly? EndDate { get; set; }
        [DefaultValue("persft")]
        [Required]
        public required string MetricType { get; set; } // Options: "persft", "peroccupancy"''
        [DefaultValue("1")]
        public int? BuildingId { get; set; } // Optional filter for building
    }
}
