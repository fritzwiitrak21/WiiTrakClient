using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class EmailHttpRepository : IEmailHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "email";
        private readonly string _apiUrl;

        public EmailHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }


        public async Task SendMailAsync(MailRequest request)
        {
           
              await _httpService.Put(_apiUrl, request);
             
        }
       
    }
}
