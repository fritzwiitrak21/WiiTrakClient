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
        private readonly IHttpService Httpservice;
        private const string ControllerName = "technicians";
        private readonly string ApiUrl;
        public TechnicianHttpRepository(IHttpService HttpService)
        {
            Httpservice = HttpService;
            ApiUrl = $"{ HttpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<TechnicianDto>> GetAllTechniciansAsync()
        {
            var response = await Httpservice.Get<List<TechnicianDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<TechnicianDto> GetTechnicianByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await Httpservice.Get<TechnicianDto>(url);
            return response.Response;
        }
        public async Task<List<TechnicianDto>> GetTechniciansBySystemOwnerIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/systemowner/{id}";
            var response = await Httpservice.Get<List<TechnicianDto>>(url);
            return response.Response;
        }
        public async Task<List<TechnicianDto>> GetTechniciansByCompanyIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/company/{id}";
            var response = await Httpservice.Get<List<TechnicianDto>>(url);
            return response.Response;
        }
        public async Task CreateTechnicianAsync(TechnicianDto technician, int RoleId)
        {
            await Httpservice.Post($"{ApiUrl}/{RoleId}", technician);
        }
        public async Task UpdateTechnicianAsync(Guid id, TechnicianDto technician, int RoleId)
        {
            await Httpservice.Put($"{ ApiUrl }/{ id }/{RoleId}", technician);
        }
        public async Task DeleteTechnicianAsync(Guid id)
        {
            await Httpservice.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
