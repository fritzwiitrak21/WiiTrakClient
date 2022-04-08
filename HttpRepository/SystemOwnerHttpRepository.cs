using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class SystemOwnerHttpRepository: ISystemOwnerHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "systemowner";
        private readonly string _apiUrl = "";

        public SystemOwnerHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<SystemOwnerDto>> GetAllSystemOwnersAsync()
        {
            var response = await _httpService.Get<List<SystemOwnerDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<SystemOwnerDto> GetSystemOwnerByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<SystemOwnerDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task<bool> CheckEmailIdAsync(string EmailId)
        {
            var response = await _httpService.Get<bool>(EmailId);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task UpdateSystemOwnerAsync(Guid id, SystemOwnerUpdateDto client)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", client);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteSystemOwnerAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
      

    }
}
