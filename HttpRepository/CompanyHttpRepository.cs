/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CompanyHttpRepository : ICompanyHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "companies";
        private readonly string _apiUrl;

        public CompanyHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var response = await _httpService.Get<List<CompanyDto>>(_apiUrl);
           
            return response.Response;
        }

        public async Task<List<CompanyDto>> GetChildCompaniesAsync(Guid id)
        {
            string url = $"{_apiUrl}/company/{id}";

            var response = await _httpService.Get<List<CompanyDto>>(url);
            
            return response.Response;
        }
        public async Task<List<CompanyDto>> GetCompaniesBySystemOwnerIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/systemowner/{id}";

            var response = await _httpService.Get<List<CompanyDto>>(url);
             
            return response.Response;
        }

        public async Task<List<CompanyDto>> GetCompaniesByCorporateIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/Corporate/{id}";

            var response = await _httpService.Get<List<CompanyDto>>(url);
             
            return response.Response;
        }



        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<CompanyDto>(url);
            
            return response.Response;
        }
        public async Task<CompanyDto> GetParentCompanyAsync(Guid subcompanyId)
        {
            string url = $"{_apiUrl}/ParentCompany/{subcompanyId}";

            var response = await _httpService.Get<CompanyDto>(url);
             
            return response.Response;
        }
        public async Task<CompanyReportDto> GetCompanyReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";

            Console.WriteLine("company report url: " + url);

            var response = await _httpService.Get<CompanyReportDto>(url);
             
            return response.Response;
        }

        public async Task CreateCompanyAsync(CompanyCreationDto company)
        {
              await _httpService.Post(_apiUrl, company);
            
        }

        public async Task UpdateCompanyAsync(Guid id, CompanyUpdateDto company)
        {

              await _httpService.Put($"{ _apiUrl }/{ id }", company);
             
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
              await _httpService.Delete($"{ _apiUrl }/{ id }");
            
        }
    }
}
