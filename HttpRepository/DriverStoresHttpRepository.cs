/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DriverStoresHttpRepository: IDriverStoresHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "driverstores";
        private readonly string _apiUrl;
        public DriverStoresHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }"; 
        }
        public async Task <List<DriverStoreDetailsDto>> GetDriverStoresByCompanyIdAsync(Guid CompanyId, Guid DriverId) 
        { 
            string url = $"{_apiUrl}/company/{CompanyId}/{DriverId}";
            var response = await _httpService.Get<List<DriverStoreDetailsDto>>(url);
            return response.Response;
        }
        public async Task<List<DriverStoreDetailsDto>> GetDriverStoresBySystemownerIdAsync(Guid SystemOwnerId, Guid DriverId)
        {
            string url = $"{_apiUrl}/systemowner/{SystemOwnerId}/{DriverId}";
            var response = await _httpService.Get<List<DriverStoreDetailsDto>>(url);
            return response.Response;
        }
        public async Task UpdateDriverStoresAsync(DriverStoreDetailsDto DriverStoreDto)
        {
              await _httpService.Put($"{ _apiUrl }", DriverStoreDto);
        }
    }
} 
