using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverStoresHttpRepository
    {
        Task<List<DriverStoreDetailsDto>> GetDriverStoresByCompanyIdAsync(Guid CompanyId, Guid DriverId);
        Task<List<DriverStoreDetailsDto>> GetDriverStoresBySystemownerIdAsync(Guid SystemOwnerId, Guid DriverId);
        Task UpdateDriverStoresAsync(Guid DriverId, DriverStoreDetailsDto _DriverStoreDto);
    }
}
