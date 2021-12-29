﻿using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CompanyHttpRepository: ICompanyHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "companies";
        private readonly string _apiUrl = "";

        public CompanyHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var response = await _httpService.Get<List<CompanyDto>>(_apiUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<CompanyDto>> GetChildCompaniesAsync(Guid id)
        {
            string url = $"{_apiUrl}/Company/{id}";

            var response = await _httpService.Get<List<CompanyDto>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<CompanyDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateCompanyAsync(CompanyCreationDto company)
        {
            var response = await _httpService.Post(_apiUrl, company);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateCompanyAsync(Guid id, CompanyUpdateDto company)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", company);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
