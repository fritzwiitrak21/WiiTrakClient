/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ISystemOwnerHttpRepository
    {
        Task<List<SystemOwnerDto>> GetAllSystemOwnersAsync();
        Task<SystemOwnerDto> GetSystemOwnerByIdAsync(Guid id);
        Task<bool> CheckEmailIdAsync(string EmailId);
        // Task CreateSystemOwnerAsync(SystemOwnerCreationDto systemOwner);
        Task UpdateSystemOwnerAsync(Guid id, SystemOwnerUpdateDto SystemOwner);
        Task DeleteSystemOwnerAsync(Guid id);
    }
}
