/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/

using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;
using WiiTrakClient.Enums;
using WiiTrakClient.Cores;

namespace WiiTrakClient.HttpRepository
{
    public class MessageHttpRepository : IMessageHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "messages";
        private readonly string _apiUrl;
        public MessageHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<MessagesDto>> GetMessagesByIdAsync(Guid Id, Role Role)
        {
            string url = $"{_apiUrl}/{Id}/{(int)Role}";
            var response = await _httpService.Get<List<MessagesDto>>(url);
            return response.Response;
        }
        public async Task AddNewMessageAsync(MessagesDto NewMessage)
        {
            await _httpService.Post(_apiUrl, NewMessage);
        }
        public async Task UpdateOldMessageAsync(Guid Id, MessagesDto NewMessage)
        {
            string url = $"{_apiUrl}/{Id}";
            await _httpService.Put(url, NewMessage);
        }
        public async Task UpdateMessageDeliveredTimeAsync()
        {
            MessagesDto dto = new MessagesDto();
            dto.Id = CurrentUser.UserId;
            await _httpService.Put($"{ _apiUrl }", dto);
        }
        public async Task UpdateMessageActionAsync(MessagesDto dto)
        {
            string url = $"{_apiUrl}/{dto.Id}";
            await _httpService.Put($"{ url }", dto);
        }
    }
}
