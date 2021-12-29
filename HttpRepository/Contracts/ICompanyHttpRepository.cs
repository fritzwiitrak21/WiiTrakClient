using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICompanyHttpRepository
    {
        Task<List<CompanyDto>> GetAllCompaniesAsync();

        Task<List<CompanyDto>> GetChildCompaniesAsync(Guid id);

        Task<CompanyDto> GetCompanyByIdAsync(Guid id);

        Task CreateCompanyAsync(CompanyCreationDto company);

        Task UpdateCompanyAsync(Guid id, CompanyUpdateDto company);

        Task DeleteCompanyAsync(Guid id);
    }
}
