namespace Dashboard_Management.DTOs
{
    public class EnergyMetricResponseDto
    {
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public string MetricType { get; set; }
        public decimal MetricValue { get; set; }
    }
}
