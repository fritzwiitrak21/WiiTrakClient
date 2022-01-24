﻿using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class TechnicianHttpRepository: ITechnicianHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "technicians";
        private readonly string _apiUrl = "";

        public TechnicianHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<TechnicianDto>> GetAllTechniciansAsync()
        {
            var response = await _httpService.Get<List<TechnicianDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<TechnicianDto> GetTechnicianByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<TechnicianDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateTechnicianAsync(TechnicianCreationDto technician)
        {
            var response = await _httpService.Post(_apiUrl, technician);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateTechnicianAsync(Guid id, TechnicianUpdateDto client)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", client);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteTechnicianAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
