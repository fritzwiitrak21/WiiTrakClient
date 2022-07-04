/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface INotificationHttpRepository
    {
        Task<List<NotificationDto>> GetNotificationIdAsync(Guid id);
        Task AddNewNotificationAsync(NotificationDto notification);
        Task UpdateNotifiedTimeAsync();
    }
}
