using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICorporateHttpRepository
    {
        Task<List<CorporateDto>> GetAllCorporatesAsync();

        Task<List<CorporateDto>> GetChildCorporatesAsync(Guid id);

        Task<CorporateDto> GetCorporateByIdAsync(Guid id);

        Task<CorporateReportDto> GetCorporateReportAsync(Guid id);

        Task CreateCorporateAsync(CorporateCreationDto corporate);

        Task UpdateCorporateAsync(Guid id, CorporateUpdateDto corporate);

        Task DeleteCorporateAsync(Guid id);
    }
}