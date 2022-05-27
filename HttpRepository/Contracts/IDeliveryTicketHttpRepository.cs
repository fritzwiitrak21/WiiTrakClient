using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IDeliveryTicketHttpRepository
    {
        Task<DeliveryTicketDto> GetDeliveryTicketByIdAsync(Guid id);

        Task<List<DeliveryTicketDto>> GetAllDeliveryTicketsAsync();

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByDriverIdAsync(Guid driverId);

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByStoreIdAsync(Guid storeId);        

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByServiceProviderIdAsync(Guid serviceProviderId);

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByCompanyIdAsync(Guid CompanyId);

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByPrimaryIdAsync(Guid Id, Role role);
        Task<List<DeliveryTicketDto>> GetReportByDateAsync(Guid Id, Role role, DateTime Startdate, DateTime Enddate);

        Task<List<DeliveryTicketDto>> GetDeliveryTicketsByCorporateIdAsync(Guid CorporateId);

        Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryAsync(Guid id);
        Task<List<DeliveryTicketDto>> GetDeliveryTicketsById(Guid Id,Role Role,int RecordCount);
        Task<List<ServiceBoardDto>> GetServiceBoardDetailsById(Guid Id, Role Role);
        //Task<DeliveryTicketDto> GetDeliveryTicketsByIdTest(DeliveryTicketInputDto inputDto);
        Task<DeliveryTicketDto> CreateDeliveryTicketAsync(DeliveryTicketCreationDto deliveryTicket);

        Task UpdateDeliveryTicketAsync(Guid id, DeliveryTicketUpdateDto deliveryTicket);

        Task DeleteDeliveryTicketAsync(Guid id);
    }
}