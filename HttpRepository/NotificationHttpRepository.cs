using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.Cores;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class NotificationHttpRepository : INotificationHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "notification";
        private readonly string _apiUrl;

        public NotificationHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<NotificationDto>> GetNotificationIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<List<NotificationDto>>(url);

            return response.Response;
        }

        public async Task AddNewNotificationAsync(NotificationDto notification)
        {
            await _httpService.Post(_apiUrl, notification);

        }

        public async Task UpdateNotifiedTimeAsync()
        {
            NotificationDto dto = new NotificationDto();
            dto.Id = CurrentUser.UserId;
            await _httpService.Put($"{ _apiUrl }", dto);

        }
    }
}
