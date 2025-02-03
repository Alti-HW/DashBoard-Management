namespace Dashboard_Management.DTOs
{
    public class EnergyConsumptionDetailDto
    {
        public int ConsumptionId { get; set; }
        public int FloorId { get; set; }
        public int BuildingId { get; set; } 
        public DateTime Timestamp { get; set; }
        public decimal EnergyConsumedKwh { get; set; }
        public decimal PeakLoadKw { get; set; }
        public decimal AvgTemperatureC { get; set; }
        public decimal Co2EmissionsKg { get; set; }

        public decimal CostPerUnit { get; set; } = 23;

        public decimal TotalCost {  get; set; }

    }

}
