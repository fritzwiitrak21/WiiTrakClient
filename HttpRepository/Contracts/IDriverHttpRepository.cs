/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDriverHttpRepository
    {
        Task<List<DriverDto>> GetAllDriversAsync();
        Task<List<DriverDto>> GetDriversByCompanyIdAsync(Guid Id);
        Task<List<DriverDto>> GetDriversBySystemOwnerIdAsync(Guid Id);
        Task<DriverDto> GetDriverByIdAsync(Guid id);
        Task<DriverReportDto> GetDriverReportAsync(Guid id);
        Task CreateDriverAsync(DriverDto driver);
        Task UpdateDriverAsync(Guid id, DriverDto driver);
        Task DeleteDriverAsync(Guid id);
    }
}
