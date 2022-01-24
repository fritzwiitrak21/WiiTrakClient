using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IWorkOrderHttpRepository
    {
        Task<WorkOrderDto> GetWorkOrderByIdAsync(Guid id);

        Task<List<WorkOrderDto>> GetAllWorkOrdersAsync();

        Task<List<WorkOrderDto>> GetWorkOrdersByStoreIdAsync(Guid storeId);

        Task<List<WorkOrderDto>> GetWorkOrdersByDriverIdAsync(Guid driverId);

        Task<List<WorkOrderDto>> GetWorkOrdersByCorporateIdAsync(Guid corporateId);

        Task<List<WorkOrderDto>> GetWorkOrdersByCompanyIdAsync(Guid companyId);        

        Task CreateWorkOrderAsync(WorkOrderCreationDto workOrder);

        Task UpdateWorkOrderAsync(Guid id, WorkOrderUpdateDto workOrder);

        Task DeleteWorkOrderAsync(Guid id);
    }
}