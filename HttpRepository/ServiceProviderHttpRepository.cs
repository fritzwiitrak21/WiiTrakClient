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
        private readonly IHttpService HttpService;
        private const string ControllerName = "serviceproviders";
        private readonly string ApiUrl;
        public ServiceProviderHttpRepository(IHttpService Httpservice)
        {
            HttpService = Httpservice;
            ApiUrl = $"{ Httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<ServiceProviderDto>> GetAllServiceProvidersAsync()
        {
            var response = await HttpService.Get<List<ServiceProviderDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<ServiceProviderDto> GetServiceProviderByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await HttpService.Get<ServiceProviderDto>(url);
            return response.Response;
        }
        public async Task CreateServiceProviderAsync(ServiceProviderCreationDto ServiceProvider)
        {
            await HttpService.Post(ApiUrl, ServiceProvider);
        }
        public async Task UpdateServiceProviderAsync(Guid id, ServiceProviderUpdateDto ServiceProvider)
        {
            await HttpService.Put($"{ ApiUrl }/{ id }", ServiceProvider);
        }
        public async Task DeleteServiceProviderAsync(Guid id)
        {
            await HttpService.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
