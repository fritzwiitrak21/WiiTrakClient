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
        private readonly IHttpService HttpService;
        private const string ControllerName = "systemowner";
        private readonly string ApiUrl;
        public SystemOwnerHttpRepository(IHttpService Httpservice)
        {
            HttpService = Httpservice;
            ApiUrl = $"{ Httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<SystemOwnerDto>> GetAllSystemOwnersAsync()
        {
            var response = await HttpService.Get<List<SystemOwnerDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<SystemOwnerDto> GetSystemOwnerByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await HttpService.Get<SystemOwnerDto>(url);
            return response.Response;
        }
        public async Task<bool> CheckEmailIdAsync(string EmailId)
        {
            var response = await HttpService.Get<bool>(EmailId);
            return response.Response;
        }
        public async Task UpdateSystemOwnerAsync(Guid id, SystemOwnerUpdateDto SystemOwner)
        {
              await HttpService.Put($"{ ApiUrl }/{ id }", SystemOwner);
        }
        public async Task DeleteSystemOwnerAsync(Guid id)
        {
             await HttpService.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
