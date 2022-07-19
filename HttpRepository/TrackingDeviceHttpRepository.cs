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
        private readonly IHttpService Httpservice;
        private const string ControllerName = "trackingdevice";
        private readonly string ApiUrl;
        public TrackingDeviceHttpRepository(IHttpService HttpService)
        {
            Httpservice = HttpService;
            ApiUrl = $"{ HttpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<TrackingDeviceDto>> GetAllTrackingDevicesAsync()
        {
            var response = await Httpservice.Get<List<TrackingDeviceDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await Httpservice.Get<TrackingDeviceDto>(url);
            return response.Response;
        }
        public async Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdDriverAsync(Guid Id)
        {
            string url = $"{ApiUrl}/CartDetails/{Id}";
            var response = await Httpservice.Get<List<TrackingDeviceDetailsDto>>(url);
            return response.Response;
        }
        public async Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdAsync(Guid id,int RoleId)
        {
            string url = $"{ApiUrl}/TrackingDevice/{id}/{RoleId}";
            var response = await Httpservice.Get<List<TrackingDeviceDetailsDto>>(url);
            return response.Response;
        }
        public async Task CreateTrackingDeviceAsync(TrackingDeviceDto TrackingDevice)
        {
            await Httpservice.Post(ApiUrl, TrackingDevice);
        }
        public async Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceDto TrackingDevice)
        {
            await Httpservice.Put($"{ ApiUrl }/{ id }", TrackingDevice);
        }
        public async Task DeleteTrackingDeviceAsync(Guid id)
        {
            await Httpservice.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
