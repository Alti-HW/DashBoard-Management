using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces;

namespace Dashboard_Management.Services
{
    public class EnergyService : IEnergyService
    {
        private readonly IEnergyRepository _energyRepository;

        public EnergyService(IEnergyRepository energyRepository)
        {
            _energyRepository = energyRepository;
        }

        public async Task<IEnumerable<BuildingEnergyConsumptionDto>> GetEnergyConsumptionAsync(EnergyConsumptionRequestDto request)
        {
            return await _energyRepository.GetEnergyConsumptionAsync(request);
        }
        public async Task<IEnumerable<EnergyMetricResponseDto>> GetMetricsAsync(MetricRequestDto request)
        {
            return await _energyRepository.GetMetricsAsync(request);
        }
    }
}
