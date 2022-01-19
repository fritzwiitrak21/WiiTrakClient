using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CorporateHttpRepository: ICorporateHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "corporates";
        private readonly string _apiUrl = "";

        public CorporateHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<CorporateDto>> GetAllCorporatesAsync()
        {
            var response = await _httpService.Get<List<CorporateDto>>(_apiUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<CorporateDto>> GetChildCorporatesAsync(Guid id)
        {
            string url = $"{_apiUrl}/corporate/{id}";

            var response = await _httpService.Get<List<CorporateDto>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<CorporateDto> GetCorporateByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<CorporateDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<CorporateReportDto> GetCorporateReportAsync(Guid id)
        {
            string url = $"{_apiUrl}/report/{id}";

            var response = await _httpService.Get<CorporateReportDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateCorporateAsync(CorporateCreationDto corporate)
        {
            var response = await _httpService.Post(_apiUrl, corporate);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateCorporateAsync(Guid id, CorporateUpdateDto corporate)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", corporate);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteCorporateAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
   
    }
}