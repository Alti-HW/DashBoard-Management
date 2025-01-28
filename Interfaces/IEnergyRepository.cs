using Dashboard_Management.DTOs;

namespace Dashboard_Management.Interfaces
{
    public interface IEnergyRepository
    {
        Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(DateTime startDate, DateTime endDate);
        Task<List<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request);
    }

}
