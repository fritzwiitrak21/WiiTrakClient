using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{    
    public class RepairIssueHttpRepository : IRepairIssueHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "repairissues";
        private readonly string _apiUrl = "";

        public RepairIssueHttpRepository(IHttpService httpService)
        {
             _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<RepairIssueDto>> GetAllRepairIssuesAsync()
        {
            var response = await _httpService.Get<List<RepairIssueDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateRepairIssueAsync(RepairIssueDto repairIssue)
        {
            var response = await _httpService.Post(_apiUrl, repairIssue);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateRepairIssueAsync(Guid id, RepairIssueDto repairIssue)
        {
            var response = await _httpService.Put($"{ _apiUrl }/{ id }", repairIssue);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteRepairIssueAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }            
    }
}