using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ITechnicianHttpRepository
    {
        Task<List<TechnicianDto>> GetAllTechniciansAsync();

        Task<TechnicianDto> GetTechnicianByIdAsync(Guid id);

        Task CreateTechnicianAsync(TechnicianCreationDto technician);

        Task UpdateTechnicianAsync(Guid id, TechnicianUpdateDto technician);

        Task DeleteTechnicianAsync(Guid id);
    }
}
