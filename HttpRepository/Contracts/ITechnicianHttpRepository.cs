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
        Task<List<TechnicianDto>> GetTechniciansBySystemOwnerIdAsync(Guid id);
        Task<List<TechnicianDto>> GetTechniciansByCompanyIdAsync(Guid id);
        Task CreateTechnicianAsync(TechnicianDto technician, int RoleId);
        Task UpdateTechnicianAsync(Guid id, TechnicianDto technician, int RoleId);
        Task DeleteTechnicianAsync(Guid id);
    }
}
