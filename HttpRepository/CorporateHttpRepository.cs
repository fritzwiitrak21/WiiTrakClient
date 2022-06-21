/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CorporateHttpRepository: ICorporateHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "corporates";
        private readonly string _apiUrl;
        public CorporateHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<CorporateDto>> GetAllCorporatesAsync()
        {
            var response = await _httpService.Get<List<CorporateDto>>(_apiUrl);
            return response.Response;
        }
        public async Task<List<CorporateDto>> GetChildCorporatesAsync(Guid id)
        {
            string url = $"{_apiUrl}/corporate/{id}";
            var response = await _httpService.Get<List<CorporateDto>>(url);
            return response.Response;
        }
        public async Task<CorporateDto> GetCorporateByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";
            var response = await _httpService.Get<CorporateDto>(url);
            return response.Response;
        }
        public async Task<List<CorporateDto>> GetCorporatesByCompanyId(Guid companyId)
        {
            string url = $"{_apiUrl}/company/{companyId}";
            var response = await _httpService.Get<List<CorporateDto>>(url);
            return response.Response;
        }
        public async Task<CorporateReportDto> GetCorporateReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";
            var response = await _httpService.Get<CorporateReportDto>(url);
            return response.Response;
        }
        public async Task CreateCorporateAsync(Guid CompanyId, int RoleId, CorporateDto corporate)
        {
            await _httpService.Post($"{_apiUrl}/{CompanyId}/{RoleId}",corporate);
        }
        public async Task UpdateCorporateAsync(Guid id, CorporateDto corporate)
        {
             await _httpService.Put($"{ _apiUrl }/{ id }", corporate);
        }
        public async Task DeleteCorporateAsync(Guid id)
        {
           await _httpService.Delete($"{ _apiUrl }/{ id }");
        }
    }
}
