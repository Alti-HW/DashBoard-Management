namespace Dashboard_Management.DTOs
{
    public class FloorEnergyConsumptionDto
    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public decimal? EnergyConsumedKwh { get; set; }
        public List<EnergyConsumptionDetailDto> FloorDetails { get; set; } = new();
    }

}
