using Dashboard_Management.DTOs;

namespace Dashboard_Management.Interfaces
{
    public interface IEnergyService
    {
        Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request);

    }
}
