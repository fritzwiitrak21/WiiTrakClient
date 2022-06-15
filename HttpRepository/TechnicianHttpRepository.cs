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

        public async Task CreateTechnicianAsync(TechnicianCreationDto technician, int RoleId)
        {
            await Httpservice.Post($"{ApiUrl}/{RoleId}", technician);
        }

        public async Task UpdateTechnicianAsync(Guid id, TechnicianUpdateDto client, int RoleId)
        {
            await Httpservice.Put($"{ ApiUrl }/{ id }/{RoleId}", client);
        }

        public async Task DeleteTechnicianAsync(Guid id)
        {
            await Httpservice.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
