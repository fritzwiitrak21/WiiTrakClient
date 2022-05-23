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
        private readonly string _apiUrl;

        public WorkOrderHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task CreateWorkOrderAsync(WorkOrderCreationDto workOrder)
        {
             var response = await _httpService.Post(_apiUrl, workOrder);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<WorkOrderDto>> GetAllWorkOrdersAsync()
        {
            System.Console.WriteLine(_apiUrl);
            var response = await _httpService.Get<List<WorkOrderDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<WorkOrderDto> GetWorkOrderByIdAsync(Guid id)
        {
             string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<WorkOrderDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
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

        public async Task UpdateWorkOrderAsync(Guid id, WorkOrderUpdateDto workOrder)
        {
             var response = await _httpService.Put($"{ _apiUrl }/{ id }", workOrder);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteWorkOrderAsync(Guid id)
        {
             var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}