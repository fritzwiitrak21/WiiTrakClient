﻿using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class SimCardsHttpRepository : ISimCardsHttpRepository
    {
        private readonly IHttpService HttpService;
        private const string ControllerName = "simcards";
        private readonly string ApiUrl;

        public SimCardsHttpRepository(IHttpService httpservice)
        {
            HttpService = httpservice;
            ApiUrl = $"{ httpservice.BaseUrl }{ ControllerName }";
        }

        public async Task<List<SimCardsDto>> GetAllSimCardDetailsAsync()
        {
            var response = await HttpService.Get<List<SimCardsDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<SimCardsDto> GetSimCardByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";

            var response = await HttpService.Get<SimCardsDto>(url);

            return response.Response;
        }

        public async Task CreateSimCardAsync(SimCardCreationDto sim)
        {
            await HttpService.Post(ApiUrl, sim);
        }

        public async Task UpdateSimCardAsync(Guid id, SimCardUpdateDto sim)
        {
            await HttpService.Put($"{ ApiUrl }/{ id }", sim);
        }
    }
}
