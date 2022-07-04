/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ISimCardsHttpRepository
    {
        Task<List<SimCardsDto>> GetAllSimCardDetailsAsync();
        Task<SimCardsDto> GetSimCardByIdAsync(Guid id);
        Task<List<SimCardsDto>> GetSimCardByTechnicianIdAsync(Guid TechnicianId);
        Task CreateSimCardAsync(SimCardsDto sim);
        Task UpdateSimCardAsync(Guid id, SimCardsDto sim);
    }
}
