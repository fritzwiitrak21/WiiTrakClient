using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IRepairIssueHttpRepository
    {
        Task<List<RepairIssueDto>> GetAllRepairIssuesAsync();

        Task CreateRepairIssueAsync(RepairIssueDto cart);

        Task UpdateRepairIssueAsync(Guid id, RepairIssueDto cart);

        Task DeleteRepairIssueAsync(Guid id);
    }
}