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
        private readonly string _apiUrl;

        public RepairIssueHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<RepairIssueDto>> GetAllRepairIssuesAsync()
        {
            var response = await _httpService.Get<List<RepairIssueDto>>(_apiUrl);

            return response.Response;
        }


        public async Task<RepairIssueDto> GetRepairIssueByIdAsync(Guid Id)
        {
            var Url = $"{_apiUrl}/{Id}";

            var response = await _httpService.Get<RepairIssueDto>(Url);

            return response.Response;
        }

        public async Task CreateRepairIssueAsync(RepairIssueDto repairIssue)
        {
            await _httpService.Post(_apiUrl, repairIssue);

        }

        public async Task UpdateRepairIssueAsync(Guid id, RepairIssueDto repairIssue)
        {
            await _httpService.Put($"{ _apiUrl }/{ id }", repairIssue);

        }

        public async Task DeleteRepairIssueAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");

        }
    }
}