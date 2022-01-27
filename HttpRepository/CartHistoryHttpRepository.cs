using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CartHistoryHttpRepository : ICartHistoryHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "carthistory";
        private readonly string _apiUrl = "";

        public CartHistoryHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

       public async Task<CartHistoryDto> GetCartHistoryByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<CartHistoryDto>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<CartHistoryDto>> GetAllCartHistoryAsync()
        {
            var response = await _httpService.Get<List<CartHistoryDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<CartHistoryDto>> GetCartHistoryByCartIdAsync(Guid cartId)
        {
            string url = $"{_apiUrl}/cart/{cartId}";

            var response = await _httpService.Get<List<CartHistoryDto>>(url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateCartHistoryAsync(CartHistoryCreationDto cartHistory)
        {
               var response = await _httpService.Post(_apiUrl, cartHistory);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateCartHistoryAsync(Guid id, CartHistoryUpdateDto cartHistory)
        {
             var response = await _httpService.Put($"{ _apiUrl }/{ id }", cartHistory);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }

         public async Task DeleteCartHistoryAsync(Guid id)
        {
             var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
        }
    }
}