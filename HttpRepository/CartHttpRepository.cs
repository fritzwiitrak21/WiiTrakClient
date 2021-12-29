using System;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CartHttpRepository: ICartHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "assets";
        private readonly string _apiUrl = "";

        public CartHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<CartDto>> GetAllCartsAsync()
        {
            var response = await _httpService.Get<List<CartDto>>(_apiUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByDriverIdAsync(Guid driverId)
        {
            string url = $"{_apiUrl}/Driver/{driverId}";

            System.Console.WriteLine("url:" + url);

            var response = await _httpService.Get<List<CartDto>>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<CartDto> GetCartByIdAsync(Guid id)
        {
            string url = $"{_apiUrl}/{id}";

            var response = await _httpService.Get<CartDto>(url);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task CreateCartAsync(CartCreationDto cart)
        {
            var response = await _httpService.Post(_apiUrl, cart);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateCartAsync(Guid id, CartUpdateDto client)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", client);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteCartAsync(Guid id)
        {
            var response = await _httpService.Delete($"{ _apiUrl }/{ id }");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}


/*
 * 
 * 


 public async Task PatchOrphanAsync(int orphanId, string propertyName, string newValue = null)
        {
            /*    JSON body example for patch request         
             [
                {
                    "value": "Smith",
                    "path": "/lastName",
                    "op": "replace"
                }
             ]             

            string json = "";
            if (newValue != null)
            {
                json = $"[{{\"value\": \"{newValue}\", \"path\": \"/{propertyName}\", \"op\": \"replace\"}}]";
            }
            else
            {
                json = $"[{{\"value\": {null}, \"path\": \"/{propertyName}\", \"op\": \"replace\"}}]";
            }

            var response = await httpService.Patch($"{ url }/{ orphanId }", json);

            if (!response.Success)
            {
                // TODO throws 404 exception in browser
                // throw new ApplicationException(await response.GetBody());
            }
        }

*/