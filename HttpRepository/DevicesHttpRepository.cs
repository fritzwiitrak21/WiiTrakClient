/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DevicesHttpRepository : IDevicesHttpRepository
    {
        private readonly IHttpService HttpService;
        private const string ControllerName = "devices";
        private readonly string ApiUrl;
        public DevicesHttpRepository(IHttpService httpservice)
        {
            HttpService = httpservice;
            ApiUrl = $"{ httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<DevicesDto>> GetAllDeviceDetailsAsync()
        {
            var response = await HttpService.Get<List<DevicesDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<DevicesDto> GetDeviceByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await HttpService.Get<DevicesDto>(url);
            return response.Response;
        }
        public async Task CreateDeviceAsync(DevicesDto device)
        {
            await HttpService.Post(ApiUrl, device);
        }
        public async Task UpdateDeviceAsync(Guid id, DevicesDto device)
        {
            await HttpService.Put($"{ ApiUrl }/{ id }", device);
        }
    }
}
