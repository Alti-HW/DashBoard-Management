namespace Dashboard_Management.DTOs.Occupancy
{
    public class BuildingOccupancyDto
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public List<FloorOccupancyDto> Floors { get; set; } = new List<FloorOccupancyDto>();
    }
}
