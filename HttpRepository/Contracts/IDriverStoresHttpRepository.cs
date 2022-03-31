using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverStoresHttpRepository
    {
        Task<List<DriverStoreDetailsDto>> GetDriverStoresByDriverIdAsync(Guid DriverId, Guid CompanyId);
    }
}
