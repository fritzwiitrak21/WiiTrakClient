/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DriverHttpRepository : IDriverHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "drivers";
        private readonly string _apiUrl;
        public DriverHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<DriverDto>> GetAllDriversAsync()
        {
            var response = await _httpService.Get<List<DriverDto>>(_apiUrl);
            return response.Response;
        }
        public async Task<List<DriverDto>> GetDriversByCompanyIdAsync(Guid Id)
        {
            var Url = $"{_apiUrl}/Company/{Id}";
            var response = await _httpService.Get<List<DriverDto>>(Url);
            return response.Response;
        }
        public async Task<List<DriverDto>> GetDriversBySystemOwnerIdAsync(Guid Id)
        {
            var Url = $"{_apiUrl}/SystemOwner/{Id}";
            var response = await _httpService.Get<List<DriverDto>>(Url);
            return response.Response;
        }
        public async Task<DriverDto> GetDriverByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";
            var response = await _httpService.Get<DriverDto>(url);
            return response.Response;
        }
        public async Task<DriverReportDto> GetDriverReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";
            var response = await _httpService.Get<DriverReportDto>(url);
            return response.Response;
        }
        public async Task CreateDriverAsync(DriverDto driver)
        {
            await _httpService.Post(_apiUrl, driver);
        }
        public async Task UpdateDriverAsync(Guid id, DriverDto driver)
        {
            await _httpService.Put($"{ _apiUrl }/{ id }", driver);
        }
        public async Task DeleteDriverAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");
        }
    }
}
