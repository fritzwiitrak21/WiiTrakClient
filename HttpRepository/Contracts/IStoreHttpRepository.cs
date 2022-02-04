using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IStoreHttpRepository
    {
        Task<List<StoreDto>> GetAllStoresAsync();

        Task<StoreDto> GetStoreByIdAsync(Guid id);

        Task<List<StoreDto>> GetStoresByServiceProviderId(Guid serviceProviderId);

        Task<List<StoreDto>> GetStoresByCorporateId(Guid corporateId);

        Task<List<StoreDto>> GetStoresByCompanyId(Guid companyId);

        Task<List<StoreDto>> GetStoresByDriverId(Guid driverId);

        Task<StoreReportDto> GetStoreReportAsync(Guid id);

        Task CreateStoreAsync(StoreCreationDto store);

        Task UpdateStoreAsync(Guid id, StoreUpdateDto store);

        Task DeleteStoreAsync(Guid id);
    }
}
