using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class ServiceProviderHttpRepository: IServiceProviderHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "serviceproviders";
        private readonly string _apiUrl = "";

        public ServiceProviderHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<ServiceProviderDto>> GetAllServiceProvidersAsync()
        {
            var response = await _httpService.Get<List<ServiceProviderDto>>(_apiUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        
        public async Task<ServiceProviderDto> GetServiceProviderByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<ServiceProviderDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateServiceProviderAsync(ServiceProviderCreationDto serviceProvider)
        {
            var response = await _httpService.Post(_apiUrl, serviceProvider);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateServiceProviderAsync(Guid id, ServiceProviderUpdateDto client)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", client);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteServiceProviderAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
