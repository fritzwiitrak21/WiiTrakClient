using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICompanyHttpRepository
    {
        Task<List<CompanyDto>> GetAllCompaniesAsync();

        Task<List<CompanyDto>> GetChildCompaniesAsync(Guid id);

        Task<CompanyDto> GetCompanyByIdAsync(Guid id);
        Task<List<CompanyDto>> GetCompaniesBySystemOwnerIdAsync(Guid id);

        Task<List<CompanyDto>> GetCompaniesByCorporateIdAsync(Guid id);
        Task<CompanyReportDto> GetCompanyReportAsync(Guid id);

        Task CreateCompanyAsync(CompanyCreationDto company);

        Task UpdateCompanyAsync(Guid id, CompanyUpdateDto company);

        Task DeleteCompanyAsync(Guid id);
    }
}
