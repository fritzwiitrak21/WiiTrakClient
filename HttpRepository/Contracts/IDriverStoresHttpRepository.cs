/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverStoresHttpRepository
    {
        Task<List<DriverStoreDetailsDto>> GetDriverStoresByCompanyIdAsync(Guid CompanyId, Guid DriverId);
        Task<List<DriverStoreDetailsDto>> GetDriverStoresBySystemownerIdAsync(Guid SystemOwnerId, Guid DriverId);
        Task UpdateDriverStoresAsync(DriverStoreDetailsDto DriverStoreDto);
    }
}
