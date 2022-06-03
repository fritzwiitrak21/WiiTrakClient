using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ISimCardsHttpRepository
    {
        Task<List<SimCardsDto>> GetAllSimCardDetailsAsync();
        Task<SimCardsDto> GetSimCardByIdAsync(Guid id);
        Task CreateSimCardAsync(SimCardCreationDto sim);
        Task UpdateSimCardAsync(Guid id, SimCardUpdateDto sim);

    }
}
