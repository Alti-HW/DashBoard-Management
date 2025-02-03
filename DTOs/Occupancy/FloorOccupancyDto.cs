namespace Dashboard_Management.DTOs.Occupancy
{
    public class FloorOccupancyDto
    {
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public int TotalOccupancyCount { get; set; }
        public decimal AvgOccupancyRatio { get; set; }
        public List<OccupancyDetailDto> OccupancyDetails { get; set; }
    }

}
