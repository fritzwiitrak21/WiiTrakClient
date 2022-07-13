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
        private readonly IHttpService Httpservice;
        private const string ControllerName = "stores";
        private readonly string ApiUrl;
        public StoreHttpRepository(IHttpService httpservice)
        {
            Httpservice = httpservice;
            ApiUrl = $"{ httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<StoreDto>> GetAllStoresAsync()
        {
            var response = await Httpservice.Get<List<StoreDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<StoreDto> GetStoreByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await Httpservice.Get<StoreDto>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByServiceProviderId(Guid ServiceProviderId)
        {
            string url = $"{ApiUrl}/ServiceProvider/{ServiceProviderId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByCorporateId(Guid CorporateId)
        {
            string url = $"{ApiUrl}/Corporate/{CorporateId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByCompanyId(Guid CompanyId)
        {
            string url = $"{ApiUrl}/Company/{CompanyId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByTechnicianId(Guid TechnicianId)
        {
            string url = $"{ApiUrl}/Technician/{TechnicianId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresBySystemOwnerId(Guid SystemownerId)
        {
            string url = $"{ApiUrl}/Systemowner/{SystemownerId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<List<StoreDto>> GetStoresByDriverId(Guid DriverId)
        {
            string url = $"{ApiUrl}/Driver/{DriverId}";
            var response = await Httpservice.Get<List<StoreDto>>(url);
            return response.Response;
        }
        public async Task<StoreReportDto> GetStoreReportAsync(Guid id)
        {
            string url = $"{ApiUrl}/report/{id}";
            var response = await Httpservice.Get<StoreReportDto>(url);
            return response.Response;
        }
        public async Task<StoreReportDto> GetAllStoreReportByDriverAsync(Guid DriverId)
        {
            string url = $"{ApiUrl}/report/driver/{DriverId}";
            var response = await Httpservice.Get<StoreReportDto>(url);
            return response.Response;
        }
        public async Task<StoreReportDto> GetAllStoreReportByCoprporateAsync(Guid CorporateId)
        {
            string url = $"{ApiUrl}/report/corporate/{CorporateId}";
            var response = await Httpservice.Get<StoreReportDto>(url);
            return response.Response;
        }
        public async Task<StoreReportDto> GetAllStoreReportByCompanyAsync(Guid CompanyId)
        {
            string url = $"{ApiUrl}/report/company/{CompanyId}";
            var response = await Httpservice.Get<StoreReportDto>(url);
            return response.Response;
        }
        public async Task CreateStoreAsync(StoreDto store)
        {
            await Httpservice.Post(ApiUrl, store);
        }
        public async Task UpdateStoreAsync(Guid id, StoreDto store)
        {
            await Httpservice.Put($"{ ApiUrl }/{ id }", store);
        }
        public async Task DeleteStoreAsync(Guid id)
        {
            await Httpservice.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
