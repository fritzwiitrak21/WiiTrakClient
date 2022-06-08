/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IRepairIssueHttpRepository
    {
        Task<List<RepairIssueDto>> GetAllRepairIssuesAsync();
        Task<RepairIssueDto> GetRepairIssueByIdAsync(Guid Id);

        Task CreateRepairIssueAsync(RepairIssueDto cart);

        Task UpdateRepairIssueAsync(Guid id, RepairIssueDto cart);

        Task DeleteRepairIssueAsync(Guid id);
    }
}