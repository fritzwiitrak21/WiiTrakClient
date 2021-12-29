using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ITrackingDeviceHttpRepository
    {
        Task<List<TrackingDeviceDto>> GetAllTrackingDevicesAsync();

        Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id);

        Task CreateTrackingDeviceAsync(TrackingDeviceCreationDto trackingDevice);

        Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceUpdateDto trackingDevice);

        Task DeleteTrackingDeviceAsync(Guid id);
    }
}