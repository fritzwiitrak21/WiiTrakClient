/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class DeliveryTicketHttpRepository : IDeliveryTicketHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "deliverytickets";
        private readonly string _apiUrl;

        public DeliveryTicketHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<DeliveryTicketDto> GetDeliveryTicketByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<DeliveryTicketDto>(url);
            
            return response.Response;
        }

        public async Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/summary/{id}";

            var response = await _httpService.Get<DeliveryTicketSummaryDto>(url);
             
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetAllDeliveryTicketsAsync()
        {
            var response = await _httpService.Get<List<DeliveryTicketDto>>(_apiUrl);
             
            return response.Response;
        }

      
        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsById(Guid Id, Role Role, int RecordCount)
        {
            string url = $"{_apiUrl}/DeliveryTickets/{Id}/{(int)Role}/{RecordCount} ";

            try
            {
                var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
                 
                return response.Response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<ServiceBoardDto>> GetServiceBoardDetailsById(Guid Id, Role Role)
        {
            string url = $"{_apiUrl}/ServiceBoard/{Id}/{(int)Role} ";

            try
            {
                var response = await _httpService.Get<List<ServiceBoardDto>>(url);
                
                return response.Response;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public async Task<DeliveryTicketDto> GetDeliveryTicketsByIdTest(DeliveryTicketInputDto inputDto)
        //{
        //    //string url = $"{_apiUrl}/DeliveryTickets/{Id}/{(int)Role}/{RecordCount}/{Fromdate}/{Todate}";
        //    string url = $"{_apiUrl}/GetDeliveryTicketsByIdTest/";
        //    var response = await _httpService.Post<DeliveryTicketInputDto, DeliveryTicketDto>(url, inputDto);
        //    try
        //    {
        //        //var response = await _httpService.Post<List<DeliveryTicketDto>>();

        //        if (!response.Success)
        //        {
        //            // throw new ApplicationException(await response.GetBody());
        //        }
        //        return response.Response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }

        //}

        public async Task<List<DeliveryTicketDto>> GetReportByDateAsync(Guid Id, Role role, DateTime Startdate, DateTime Enddate)
        {
            string url = $"{_apiUrl}/Report/{Id}/{(int)role}/{Startdate}/{Enddate}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
            
            return response.Response;
        }

        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByCorporateIdAsync(Guid CorporateId)
        {
            string url = $"{_apiUrl}/Corporate/{CorporateId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
             
            return response.Response;
        }
        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByCompanyIdAsync(Guid CompanyId)
        {
            string url = $"{_apiUrl}/company/{CompanyId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
             
            return response.Response;
        }
        public async Task<List<DeliveryTicketDto>> GetDeliveryTicketsByServiceProviderIdAsync(Guid serviceProviderId)
        {
            string url = $"{_apiUrl}/serviceprovider/{serviceProviderId}";

            var response = await _httpService.Get<List<DeliveryTicketDto>>(url);
            
            return response.Response;
        }

        public async Task<DeliveryTicketSummaryDto> GetDeliveryTicketSummaryAsync(Guid id)
        {
            string url = $"{_apiUrl}/Summary/{id}";

            var response = await _httpService.Get<DeliveryTicketSummaryDto>(url);
             
            return response.Response;
        }

        public async Task<DeliveryTicketDto> CreateDeliveryTicketAsync(DeliveryTicketCreationDto deliveryTicket)
        {
            var response = await _httpService.Post<DeliveryTicketCreationDto, DeliveryTicketDto>(_apiUrl, deliveryTicket);

            return response.Response;
        }

        public async Task UpdateDeliveryTicketAsync(Guid id, DeliveryTicketUpdateDto deliveryTicket)
        {
             await _httpService.Put($"{ _apiUrl }/{ id }", deliveryTicket);
             
            }

        public async Task DeleteDeliveryTicketAsync(Guid id)
        {
            await _httpService.Delete($"{ _apiUrl }/{ id }");
             
            }
        }
}