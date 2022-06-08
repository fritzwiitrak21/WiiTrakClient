/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDevicesHttpRepository
    {
        Task<List<DevicesDto>> GetAllDeviceDetailsAsync();
        Task<DevicesDto> GetDeviceByIdAsync(Guid id);
        Task CreateDeviceAsync(DeviceCreationDto device);
        Task UpdateDeviceAsync(Guid id, DeviceUpdateDto device);
    }
}
