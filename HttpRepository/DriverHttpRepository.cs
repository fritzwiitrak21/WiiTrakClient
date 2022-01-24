using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DriverHttpRepository: IDriverHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "drivers";
        private readonly string _apiUrl = "";

        public DriverHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<DriverDto>> GetAllDriversAsync()
        {
            System.Console.WriteLine(_apiUrl);
            var response = await _httpService.Get<List<DriverDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<DriverDto> GetDriverByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<DriverDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateDriverAsync(DriverCreationDto driver)
        {
            var response = await _httpService.Post(_apiUrl, driver);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateDriverAsync(Guid id, DriverUpdateDto driver)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", driver);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteDriverAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
