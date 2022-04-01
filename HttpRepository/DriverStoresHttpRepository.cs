using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DriverStoresHttpRepository: IDriverStoresHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "driverstores";
        private readonly string _apiUrl = "";

        public DriverStoresHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }"; 
        }
        public async Task <List<DriverStoreDetailsDto>> GetDriverStoresByCompanyIdAsync(Guid CompanyId, Guid DriverId) 
        { 
            string url = $"{_apiUrl}/company/{CompanyId}/{DriverId}";

            var response = await _httpService.Get<List<DriverStoreDetailsDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task<List<DriverStoreDetailsDto>> GetDriverStoresBySystemownerIdAsync(Guid SystemOwnerId, Guid DriverId)
        {
            string url = $"{_apiUrl}/systemowner/{SystemOwnerId}/{DriverId}";

            var response = await _httpService.Get<List<DriverStoreDetailsDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task UpdateDriverStoresAsync(Guid DriverId,DriverStoreDetailsDto _DriverStoreDto)
        {
            var response = await _httpService.Put($"{ _apiUrl }/{ DriverId }", _DriverStoreDto);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
} 
