/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IServiceProviderHttpRepository
    {
        Task<List<ServiceProviderDto>> GetAllServiceProvidersAsync();
        Task<ServiceProviderDto> GetServiceProviderByIdAsync(Guid id);
        Task CreateServiceProviderAsync(ServiceProviderCreationDto serviceProvider);
        Task UpdateServiceProviderAsync(Guid id, ServiceProviderUpdateDto serviceProvider);
        Task DeleteServiceProviderAsync(Guid id);
    }
}
