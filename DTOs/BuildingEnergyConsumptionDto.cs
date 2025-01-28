namespace Dashboard_Management.DTOs
{
    public class BuildingEnergyConsumptionDto
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public decimal? TotalEnergyConsumedKwh { get; set; }
        public List<FloorEnergyConsumptionDto> FloorConsumptions { get; set; }
    }

}
