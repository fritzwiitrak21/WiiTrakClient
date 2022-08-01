/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CartHistoryHttpRepository : ICartHistoryHttpRepository
    {
        private readonly IHttpService HttpService;
        private const string ControllerName = "carthistory";
        private readonly string ApiUrl;
        public CartHistoryHttpRepository(IHttpService Httpservice)
        {
            HttpService = Httpservice;
            ApiUrl = $"{ Httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<CartHistoryDto> GetCartHistoryByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await HttpService.Get<CartHistoryDto>(url);
            return response.Response;
        }
        public async Task<List<CartHistoryDto>> GetAllCartHistoryAsync()
        {
            var response = await HttpService.Get<List<CartHistoryDto>>(ApiUrl);
            return response.Response;
        }
        public async Task<List<CartHistoryDto>> GetCartHistoryByCartIdAsync(Guid CartId)
        {
            string url = $"{ApiUrl}/cart/{CartId}";
            var response = await HttpService.Get<List<CartHistoryDto>>(url);
            return response.Response;
        }
        public async Task<List<CartDto>> GetCartHistoryByDeliveryTicketIdAsync(Guid DeliveryTicketId)
        {
            string url = $"{ApiUrl}/CartHistory/{DeliveryTicketId}";
            var response = await HttpService.Get<List<CartDto>>(url);
            return response.Response;
        }
        public async Task CreateCartHistoryAsync(CartHistoryCreationDto cart)
        {
              await HttpService.Post(ApiUrl, cart);
        }
        public async Task UpdateCartHistoryAsync(Guid id, CartHistoryUpdateDto cart)
        {
             await HttpService.Put($"{ ApiUrl }/{ id }", cart);
        }
        public async Task DeleteCartHistoryAsync(Guid id)
        {
             await HttpService.Delete($"{ ApiUrl }/{ id }");
        }
    }
}
