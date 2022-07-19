/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CorporateHttpRepository: ICorporateHttpRepository
    {
        private readonly IHttpService Httpservice;
        private const string ControllerName = "corporates";
        private readonly string ApiUrl;
        public CorporateHttpRepository(IHttpService HttpService)
        {
            Httpservice = HttpService;
            ApiUrl = $"{ HttpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<CorporateDto>> GetAllCorporatesAsync()
        {
            var response = await Httpservice.Get<List<CorporateDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<List<CorporateDto>> GetChildCorporatesAsync(Guid id)
        {
            string url = $"{ApiUrl}/corporate/{id}";
            var response = await Httpservice.Get<List<CorporateDto>>(url);
            return response.Response;
        }
        public async Task<CorporateDto> GetCorporateByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await Httpservice.Get<CorporateDto>(url);
            return response.Response;
        }
        public async Task<List<CorporateDto>> GetCorporatesByCompanyId(Guid CompanyId)
        {
            string url = $"{ApiUrl}/company/{CompanyId}";
            var response = await Httpservice.Get<List<CorporateDto>>(url);
            return response.Response;
        }
        public async Task<CorporateReportDto> GetCorporateReportAsync(Guid id)
        {
            string url = $"{ApiUrl}/report/{id}";
            var response = await Httpservice.Get<CorporateReportDto>(url);
            return response.Response;
        }
        public async Task CreateCorporateAsync(Guid CompanyId, int RoleId, CorporateDto corporate)
        {
            await Httpservice.Post($"{ApiUrl}/{CompanyId}/{RoleId}",corporate);
        }
        public async Task UpdateCorporateAsync(Guid id, CorporateDto corporate)
        {
             await Httpservice.Put($"{ ApiUrl }/{ id }", corporate);
        }
        public async Task DeleteCorporateAsync(Guid id)
        {
           await Httpservice.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
