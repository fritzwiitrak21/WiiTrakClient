using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDeliveryTicketHttpRepository
    {
        Task<DeliveryTicketDto> GetDeliveryTicketByIdAsync(Guid id);

        Task<List<DeliveryTicketDto>> GetAllDeliveryTicketsAsync();

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByDriverIdAsync(Guid driverId);

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByStoreIdAsync(Guid storeId);        

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByServiceProviderIdAsync(Guid serviceProviderId);

        Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryAsync(Guid id);

        Task<DeliveryTicketDto> CreateDeliveryTicketAsync(DeliveryTicketCreationDto deliveryTicket);

        Task UpdateDeliveryTicketAsync(Guid id, DeliveryTicketUpdateDto deliveryTicket);

        Task DeleteDeliveryTicketAsync(Guid id);
    }
}