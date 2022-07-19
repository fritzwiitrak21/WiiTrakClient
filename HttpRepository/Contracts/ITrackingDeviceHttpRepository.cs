/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ITrackingDeviceHttpRepository
    {
        Task<List<TrackingDeviceDto>> GetAllTrackingDevicesAsync();
        Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id);
        Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdDriverAsync(Guid Id);
        Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdAsync(Guid id, int RoleId);
        Task CreateTrackingDeviceAsync(TrackingDeviceDto TrackingDevice);
        Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceDto TrackingDevice);
        Task DeleteTrackingDeviceAsync(Guid id);
    }
}
