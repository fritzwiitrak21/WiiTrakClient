using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class CartHttpRepository : ICartHttpRepository
    {
        private readonly IHttpService HttpService;
        private const string ControllerName = "carts";
        private readonly string _apiUrl;

        public CartHttpRepository(IHttpService httpService)
        {
            HttpService = httpService;
            ApiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<CartDto>> GetAllCartsAsync()
        {
            var response = await HttpService.Get<List<CartDto>>(ApiUrl);
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByDeliveryTicketIdAsync(Guid deliveryTicketId)
        {
            string url = $"{ApiUrl}/DeliveryTicket/{deliveryTicketId}";

            System.Console.WriteLine("url:" + url);

            var response = await HttpService.Get<List<CartDto>>(url);
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByStoreIdAsync(Guid storeId)
        {
            string url = $"{ApiUrl}/store/{storeId}";

            System.Console.WriteLine("url:" + url);

            var response = await HttpService.Get<List<CartDto>>(url);
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByDriverIdAsync(Guid driverId)
        {
            string url = $"{ApiUrl}/driver/{driverId}";

            System.Console.WriteLine("url:" + url);

            var response = await HttpService.Get<List<CartDto>>(url);
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByCorporateIdAsync(Guid corporateId)
        {
            string url = $"{ApiUrl}/corporate/{corporateId}";

            System.Console.WriteLine("url:" + url);

            var response = await HttpService.Get<List<CartDto>>(url);
            return response.Response;
        }

        public async Task<List<CartDto>> GetCartsByCompanyIdAsync(Guid companyId)
        {
            string url = $"{ApiUrl}/company/{companyId}";

            System.Console.WriteLine("url:" + url);

            var response = await HttpService.Get<List<CartDto>>(url);

            return response.Response;
        }

        public async Task<CartDto> GetCartByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";

            var response = await HttpService.Get<CartDto>(url);

            return response.Response;
        }

        public async Task CreateCartAsync(CartCreationDto cart)
        {
            await _httpService.Post(_apiUrl, cart);
            
        }

        public async Task UpdateCartAsync(Guid id, CartUpdateDto cart)
        {

             await _httpService.Put($"{ _apiUrl }/{ id }", cart);

        }

        public async Task DeleteCartAsync(Guid id)
        {
             await _httpService.Delete($"{ _apiUrl }/{ id }");

        }
    }
}
