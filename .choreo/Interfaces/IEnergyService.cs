using Dashboard_Management.DTOs;

namespace Dashboard_Management.Interfaces
{
    public interface IEnergyService
    {
        Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(EnergyConsumptionRequestDto request);
        Task<IEnumerable<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request);

    }
}
