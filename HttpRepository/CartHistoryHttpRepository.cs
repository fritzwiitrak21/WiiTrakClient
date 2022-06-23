/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CartHistoryHttpRepository : ICartHistoryHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "carthistory";
        private readonly string _apiUrl;
        public CartHistoryHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<CartHistoryDto> GetCartHistoryByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";
            var response = await _httpService.Get<CartHistoryDto>(url);
            return response.Response;
        }
        public async Task<List<CartHistoryDto>> GetAllCartHistoryAsync()
        {
            var response = await _httpService.Get<List<CartHistoryDto>>(_apiUrl);
            return response.Response;
        }
        public async Task<List<CartHistoryDto>> GetCartHistoryByCartIdAsync(Guid cartId)
        {
            string url = $"{_apiUrl}/cart/{cartId}";
            var response = await _httpService.Get<List<CartHistoryDto>>(url);
            return response.Response;
        }
        public async Task<List<CartDto>> GetCartHistoryByDeliveryTicketIdAsync(Guid deliveryTicketId)
        {
            string url = $"{_apiUrl}/CartHistory/{deliveryTicketId}";

            System.Console.WriteLine("url:" + url);

            var response = await _httpService.Get<List<CartDto>>(url);
            return response.Response;
        }
        public async Task CreateCartHistoryAsync(CartHistoryCreationDto cartHistory)
        {
              await _httpService.Post(_apiUrl, cartHistory);
        }
        public async Task UpdateCartHistoryAsync(Guid id, CartHistoryUpdateDto cartHistory)
        {
             await _httpService.Put($"{ _apiUrl }/{ id }", cartHistory);
        }
        public async Task DeleteCartHistoryAsync(Guid id)
        {
             await _httpService.Delete($"{ _apiUrl }/{ id }");
        }
    }
}
