/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class TechnicianHttpRepository : ITechnicianHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "technicians";
        private readonly string _apiUrl;

        public TechnicianHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<TechnicianDto>> GetAllTechniciansAsync()
        {
            var response = await _httpService.Get<List<TechnicianDto>>(_apiUrl);

            return response.Response;
        }

        public async Task<TechnicianDto> GetTechnicianByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<TechnicianDto>(url);

            return response.Response;
        }

        public async Task CreateTechnicianAsync(TechnicianCreationDto technician)
        {
            await _httpService.Post(_apiUrl, technician);

        }

        public async Task UpdateTechnicianAsync(Guid id, TechnicianUpdateDto client)
        {

            await _httpService.Put($"{ _apiUrl }/{ id }", client);

        }

        public async Task DeleteTechnicianAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");

        }
    }
}
