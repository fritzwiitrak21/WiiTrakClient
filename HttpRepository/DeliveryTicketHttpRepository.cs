using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DeliveryTicketHttpRepository : IDeliveryTicketHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "deliverytickets";
        private readonly string _apiUrl = "";

        public DeliveryTicketHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        
        public async Task<DeliveryTicketDto> GetDeliveryTicketByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<DeliveryTicketDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

          public async Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/summary/{id}";

            var response = await _httpService.Get<DeliveryTicketSummaryDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetAllDeliveryTicketsAsync()
        {
            var response = await _httpService.Get<List<DeliveryTicketDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByDriverIdAsync(Guid driverId)
        {
            string url = $"{_apiUrl}/driver/{driverId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByStoreIdAsync(Guid storeId)
        {
            string url = $"{_apiUrl}/store/{storeId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByServiceProviderIdAsync(Guid serviceProviderId)
        {
            string url = $"{_apiUrl}/serviceprovider/{serviceProviderId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryAsync(Guid id)
        {
            string url = $"{_apiUrl}/Summary/{id}";

            var response = await _httpService.Get<DeliveryTicketSummaryDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<DeliveryTicketDto> CreateDeliveryTicketAsync(DeliveryTicketCreationDto deliveryTicket)
        {
            var response = await _httpService.Post<DeliveryTicketCreationDto, DeliveryTicketDto>(_apiUrl, deliveryTicket);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task UpdateDeliveryTicketAsync(Guid id, DeliveryTicketUpdateDto deliveryTicket)
        {
             var response = await _httpService.Put($"{ _apiUrl }/{ id }", deliveryTicket);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteDeliveryTicketAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}