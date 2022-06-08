/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class StoreHttpRepository : IStoreHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "stores";
        private readonly string _apiUrl;

        public StoreHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<StoreDto>> GetAllStoresAsync()
        {
            var response = await _httpService.Get<List<StoreDto>>(_apiUrl);

            return response.Response;
        }
        public async Task<StoreDto> GetStoreByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<StoreDto>(url);

            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByServiceProviderId(Guid serviceProviderId)
        {
            string url = $"{_apiUrl}/ServiceProvider/{serviceProviderId}";
            var response = await _httpService.Get<List<StoreDto>>(url);

            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByCorporateId(Guid corporateId)
        {
            string url = $"{_apiUrl}/Corporate/{corporateId}";
            var response = await _httpService.Get<List<StoreDto>>(url);

            return response.Response;
        }

        public async Task<List<StoreDto>> GetStoresByCompanyId(Guid companyId)
        {
            string url = $"{_apiUrl}/Company/{companyId}";
            var response = await _httpService.Get<List<StoreDto>>(url);

            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresBySystemOwnerId(Guid SystemownerId)
        {
            string url = $"{_apiUrl}/Systemowner/{SystemownerId}";
            var response = await _httpService.Get<List<StoreDto>>(url);

            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByDriverId(Guid driverId)
        {
            string url = $"{_apiUrl}/Driver/{driverId}";
            var response = await _httpService.Get<List<StoreDto>>(url);

            return response.Response;
        }
        public async Task<StoreReportDto> GetStoreReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";

            var response = await _httpService.Get<StoreReportDto>(url);

            return response.Response;
        }

        public async Task<StoreReportDto> GetAllStoreReportByDriverAsync(Guid driverId)
        {
            string url = $"{_apiUrl}/report/driver/{driverId}";

            var response = await _httpService.Get<StoreReportDto>(url);

            return response.Response;
        }

        public async Task<StoreReportDto> GetAllStoreReportByCoprporateAsync(Guid corporateId)
        {
            string url = $"{_apiUrl}/report/corporate/{corporateId}";

            var response = await _httpService.Get<StoreReportDto>(url);

            return response.Response;
        }

        public async Task<StoreReportDto> GetAllStoreReportByCompanyAsync(Guid companyId)
        {
            string url = $"{_apiUrl}/report/company/{companyId}";

            var response = await _httpService.Get<StoreReportDto>(url);

            return response.Response;
        }

        public async Task CreateStoreAsync(StoreCreationDto store)
        {
            await _httpService.Post(_apiUrl, store);

        }

        public async Task UpdateStoreAsync(Guid id, StoreUpdateDto client)
        {

            await _httpService.Put($"{ _apiUrl }/{ id }", client);

        }

        public async Task DeleteStoreAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");

        }
    }
}
