namespace Dashboard_Management.DTOs.Occupancy
{
    public class OccupancyDetailDto
    {
        public int OccupancyId { get; set; }
        public int FloorId { get; set; }
        public DateTime Timestamp { get; set; }
        public int OccupancyCount { get; set; }
        public decimal AvgOccupancyRatio { get; set; }
    }

}
