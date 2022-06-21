/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class ServiceProviderHttpRepository : IServiceProviderHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "serviceproviders";
        private readonly string _apiUrl;
        public ServiceProviderHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<ServiceProviderDto>> GetAllServiceProvidersAsync()
        {
            var response = await _httpService.Get<List<ServiceProviderDto>>(_apiUrl);
            return response.Response;
        }
        public async Task<ServiceProviderDto> GetServiceProviderByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";
            var response = await _httpService.Get<ServiceProviderDto>(url);
            return response.Response;
        }
        public async Task CreateServiceProviderAsync(ServiceProviderCreationDto serviceProvider)
        {
            await _httpService.Post(_apiUrl, serviceProvider);
        }
        public async Task UpdateServiceProviderAsync(Guid id, ServiceProviderUpdateDto client)
        {
            await _httpService.Put($"{ _apiUrl }/{ id }", client);
        }
        public async Task DeleteServiceProviderAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");
        }
    }
}
