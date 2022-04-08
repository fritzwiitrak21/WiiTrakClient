using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ISystemOwnerHttpRepository
    {
        Task<List<SystemOwnerDto>> GetAllSystemOwnersAsync();

        Task<SystemOwnerDto> GetSystemOwnerByIdAsync(Guid id);
        Task<bool> CheckEmailIdAsync(string EmailId);
        // Task CreateSystemOwnerAsync(SystemOwnerCreationDto systemOwner);

        Task UpdateSystemOwnerAsync(Guid id, SystemOwnerUpdateDto systemOwner);

        Task DeleteSystemOwnerAsync(Guid id);

        
    }
}
