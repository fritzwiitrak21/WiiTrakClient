/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class RepairIssueHttpRepository : IRepairIssueHttpRepository
    {
        private readonly IHttpService HttpService;
        private const string ControllerName = "repairissues";
        private readonly string ApiUrl;
        public RepairIssueHttpRepository(IHttpService Httpservice)
        {
            HttpService = Httpservice;
            ApiUrl = $"{ Httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<RepairIssueDto>> GetAllRepairIssuesAsync()
        {
            var response = await HttpService.Get<List<RepairIssueDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<RepairIssueDto> GetRepairIssueByIdAsync(Guid Id)
        {
            var Url = $"{ApiUrl}/{Id}";
            var response = await HttpService.Get<RepairIssueDto>(Url);
            return response.Response;
        }
        public async Task CreateRepairIssueAsync(RepairIssueDto RepairIssue)
        {
            await HttpService.Post(ApiUrl, RepairIssue);
        }
        public async Task UpdateRepairIssueAsync(Guid id, RepairIssueDto RepairIssue)
        {
            await HttpService.Put($"{ ApiUrl }/{ id }", RepairIssue);
        }
        public async Task DeleteRepairIssueAsync(Guid id)
        {
            await HttpService.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
