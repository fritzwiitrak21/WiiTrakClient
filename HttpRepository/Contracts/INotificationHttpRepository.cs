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
