using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class TrackingDeviceHttpRepository: ITrackingDeviceHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "trackingdevice";
        private readonly string _apiUrl;

        public TrackingDeviceHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<TrackingDeviceDto>> GetAllTrackingDevicesAsync()
        {
            var response = await _httpService.Get<List<TrackingDeviceDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<TrackingDeviceDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateTrackingDeviceAsync(TrackingDeviceCreationDto trackingDevice)
        {
            var response = await _httpService.Post(_apiUrl, trackingDevice);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceUpdateDto trackingDevice)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", trackingDevice);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteTrackingDeviceAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
    
}