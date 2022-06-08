/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICorporateHttpRepository
    {
        Task<List<CorporateDto>> GetAllCorporatesAsync();

        Task<List<CorporateDto>> GetChildCorporatesAsync(Guid id);

        Task<CorporateDto> GetCorporateByIdAsync(Guid id);

        Task<List<CorporateDto>> GetCorporatesByCompanyId(Guid companyId);

        Task<CorporateReportDto> GetCorporateReportAsync(Guid id);

        Task CreateCorporateAsync(Guid CompanyId, int RoleId, CorporateCreationDto corporate);

        Task UpdateCorporateAsync(Guid id, CorporateUpdateDto corporate);

        Task DeleteCorporateAsync(Guid id);
    }
}