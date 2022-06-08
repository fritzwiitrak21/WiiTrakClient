/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class SystemOwnerHttpRepository: ISystemOwnerHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "systemowner";
        private readonly string _apiUrl;

        public SystemOwnerHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<SystemOwnerDto>> GetAllSystemOwnersAsync()
        {
            var response = await _httpService.Get<List<SystemOwnerDto>>(_apiUrl);
          
            return response.Response;
        }

        public async Task<SystemOwnerDto> GetSystemOwnerByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<SystemOwnerDto>(url);
             
            return response.Response;
        }
        public async Task<bool> CheckEmailIdAsync(string EmailId)
        {
            var response = await _httpService.Get<bool>(EmailId);
             
            return response.Response;
        }
        public async Task UpdateSystemOwnerAsync(Guid id, SystemOwnerUpdateDto client)
        {

              await _httpService.Put($"{ _apiUrl }/{ id }", client);
             
        }

        public async Task DeleteSystemOwnerAsync(Guid id)
        {
             await _httpService.Delete($"{ _apiUrl }/{ id }");
            
        }
       
    }
}
