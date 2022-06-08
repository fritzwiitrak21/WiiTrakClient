/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ITechnicianHttpRepository
    {
        Task<List<TechnicianDto>> GetAllTechniciansAsync();

        Task<TechnicianDto> GetTechnicianByIdAsync(Guid id);

        Task CreateTechnicianAsync(TechnicianCreationDto technician);

        Task UpdateTechnicianAsync(Guid id, TechnicianUpdateDto technician);

        Task DeleteTechnicianAsync(Guid id);
    }
}
