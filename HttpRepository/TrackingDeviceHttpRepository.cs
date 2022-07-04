/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class TrackingDeviceHttpRepository : ITrackingDeviceHttpRepository
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
            return response.Response;
        }
        public async Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";
            var response = await _httpService.Get<TrackingDeviceDto>(url);
            return response.Response;
        }
        public async Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdAsync(Guid id,int RoleId)
        {
            string url = $"{_apiUrl}/TrackingDevice/{id}/{RoleId}";
            var response = await _httpService.Get<List<TrackingDeviceDetailsDto>>(url);
            return response.Response;
        }
        public async Task CreateTrackingDeviceAsync(TrackingDeviceCreationDto trackingDevice)
        {
            await _httpService.Post(_apiUrl, trackingDevice);
        }
        public async Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceUpdateDto trackingDevice)
        {
            await _httpService.Put($"{ _apiUrl }/{ id }", trackingDevice);
        }
        public async Task DeleteTrackingDeviceAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");
        }
    }
}
