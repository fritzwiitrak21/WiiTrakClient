using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverHttpRepository
    {
        Task<List<DriverDto>> GetAllDriversAsync();
        Task<List<DriverDto>> GetDriversByCompanyIdAsync(Guid Id);
        Task<List<DriverDto>> GetDriversBySystemOwnerIdAsync(Guid Id);

        Task<DriverDto> GetDriverByIdAsync(Guid id);

        Task<DriverReportDto> GetDriverReportAsync(Guid id);

        Task CreateDriverAsync(DriverCreationDto driver);

        Task UpdateDriverAsync(Guid id, DriverUpdateDto driver);

        Task DeleteDriverAsync(Guid id);
    }
}
