using Dashboard_Management.DTOs;

namespace Dashboard_Management.Interfaces
{
    public interface IEnergyRepository
    {
        Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(EnergyConsumptionRequestDto request);
        Task<List<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request);
    }

}
