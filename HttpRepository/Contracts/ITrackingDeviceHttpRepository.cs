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

        Task CreateTrackingDeviceAsync(TrackingDeviceCreationDto trackingDevice);

        Task UpdateTrackingDeviceAsync(Guid id, TrackingDeviceUpdateDto trackingDevice);

        Task DeleteTrackingDeviceAsync(Guid id);
    }
}