using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class WorkOrderHttpRepository : IWorkOrderHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "workorders";
        private readonly string _apiUrl = "";

        public WorkOrderHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public Task CreateWorkOrderAsync(WorkOrderCreationDto workOrder)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWorkOrderAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrderDto>> GetAllWorkOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkOrderDto> GetWorkOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrderDto>> GetWorkOrdersByCompanyIdAsync(Guid companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrderDto>> GetWorkOrdersByCorporateIdAsync(Guid corporateId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrderDto>> GetWorkOrdersByDriverIdAsync(Guid driverId)
        {
            throw new NotImplementedException();
        }

        public Task<List<WorkOrderDto>> GetWorkOrdersByStoreIdAsync(Guid storeId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWorkOrderAsync(Guid id, WorkOrderUpdateDto workOrder)
        {
            throw new NotImplementedException();
        }
    }
}