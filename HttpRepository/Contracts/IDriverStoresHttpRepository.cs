/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverStoresHttpRepository
    {
        Task<List<DriverStoreDetailsDto>> GetDriverStoresByCompanyIdAsync(Guid CompanyId, Guid DriverId);
        Task<List<DriverStoreDetailsDto>> GetDriverStoresBySystemownerIdAsync(Guid SystemOwnerId, Guid DriverId);
        Task<List<DriverStoreHistoryDto>> GetDriverAssignHistoryByIdAsync(Guid UserId, Role Role);
        Task UpdateDriverStoresAsync(DriverStoreDetailsDto DriverStoreDto);
    }
}
