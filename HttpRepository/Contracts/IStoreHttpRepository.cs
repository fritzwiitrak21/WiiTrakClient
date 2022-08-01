/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IStoreHttpRepository
    {
        Task<List<StoreDto>> GetAllStoresAsync();
        Task<StoreDto> GetStoreByIdAsync(Guid id);
        Task<List<StoreDto>> GetStoresByServiceProviderId(Guid ServiceProviderId);
        Task<List<StoreDto>> GetStoresByCorporateId(Guid CorporateId);
        Task<List<StoreDto>> GetStoresByCompanyId(Guid CompanyId);
        Task<List<StoreDto>> GetStoresBySystemOwnerId(Guid SystemownerId);
        Task<List<StoreDto>> GetStoresByDriverId(Guid DriverId);
        Task<StoreReportDto> GetStoreReportAsync(Guid id);
        Task<StoreReportDto> GetAllStoreReportByDriverAsync(Guid DriverId);
        Task<StoreReportDto> GetAllStoreReportByCoprporateAsync(Guid CorporateId);
        Task<StoreReportDto> GetAllStoreReportByCompanyAsync(Guid CompanyId);
        Task<List<StoreDto>> GetStoresByTechnicianId(Guid TechnicianId);
        Task<List<StoreUpdateHistoryDto>> GetStoreUpdateHistoryByIdAsync(Guid UserId, Role Role);
        Task CreateStoreAsync(StoreDto store);
        Task UpdateStoreAsync(Guid id, StoreDto store);
        Task UpdateStoreFenceAsync(StoreDto store);
        Task DeleteStoreAsync(Guid id);
    }
}
