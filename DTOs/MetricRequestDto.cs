using System;

namespace Dashboard_Management.DTOs
{
    public class MetricRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MetricType { get; set; } // Options: "persft", "peroccupancy"''
        public int? BuildingId { get; set; } // Optional filter for building
    }
}
