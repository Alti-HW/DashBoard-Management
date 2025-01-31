namespace Dashboard_Management.DTOs.Building
{
    public class BuildingMinimalDto
    {
        public int BuildingId { get; set; }
        public string? Name { get; set; }
        public List<FloorMinimalDto>? Floors { get; set; }
    }
}
