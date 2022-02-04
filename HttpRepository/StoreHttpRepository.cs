using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class StoreHttpRepository: IStoreHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "stores";
        private readonly string _apiUrl = "";

        public StoreHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<StoreDto>> GetAllStoresAsync()
        {
            var response = await _httpService.Get<List<StoreDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task<StoreDto> GetStoreByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<StoreDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByServiceProviderId(Guid serviceProviderId)
        {
            string url = $"{_apiUrl}/ServiceProvider/{serviceProviderId}";
            var response = await _httpService.Get<List<StoreDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByCorporateId(Guid corporateId)
        {
              string url = $"{_apiUrl}/Corporate/{corporateId}";
            var response = await _httpService.Get<List<StoreDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByCompanyId(Guid companyId)
        {
            string url = $"{_apiUrl}/Company/{companyId}";
            var response = await _httpService.Get<List<StoreDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

         public async Task<List<StoreDto>> GetStoresByDriverId(Guid driverId)
        {
            string url = $"{_apiUrl}/Driver/{driverId}";
            var response = await _httpService.Get<List<StoreDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task<StoreReportDto> GetStoreReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";

            var response = await _httpService.Get<StoreReportDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateStoreAsync(StoreCreationDto store)
        {
            var response = await _httpService.Post(_apiUrl, store);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateStoreAsync(Guid id, StoreUpdateDto client)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", client);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
