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
        public async Task <List<DriverStoreDetailsDto>> GetDriverStoresByDriverIdAsync(Guid DriverId,Guid CompanyId) 
        {
            string url = $"{_apiUrl}/{DriverId}/{CompanyId}";

            var response = await _httpService.Get<List<DriverStoreDetailsDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
      
    }
} 
