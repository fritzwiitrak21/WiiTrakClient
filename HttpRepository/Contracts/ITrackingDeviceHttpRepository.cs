/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ITrackingDeviceHttpRepository
    {
        Task<List<TrackingDeviceDto>> GetAllTrackingDevicesAsync();

        Task<TrackingDeviceDto> GetTrackingDeviceByIdAsync(Guid id);
        Task<List<TrackingDeviceDetailsDto>> GetTrackingDeviceDetailsByIdAsync(Guid id, int RoleId);
        Task CreateTrackingDeviceAsync(TrackingDeviceCreationDto trackingDevice);

        Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceUpdateDto trackingDevice);

        Task DeleteTrackingDeviceAsync(Guid id);
    }
}