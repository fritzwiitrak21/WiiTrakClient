using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IStoreHttpRepository
    {
        Task<List<StoreDto>> GetAllStoresAsync();

        Task<StoreDto> GetStoreByIdAsync(Guid id);

        Task CreateStoreAsync(StoreCreationDto store);

        Task UpdateStoreAsync(Guid id, StoreUpdateDto store);

        Task DeleteStoreAsync(Guid id);
    }
}
