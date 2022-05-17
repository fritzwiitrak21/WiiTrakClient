using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;
namespace WiiTrakClient.HttpRepository
{
    public class CountyCodeHttpRepository:ICountyCodeHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "countycode";
        private readonly string _apiUrl;
        public CountyCodeHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }
        public async Task<List<CountyCodeDTO>> GetCountyListAsync()
        {
            var response = await _httpService.Get<List<CountyCodeDTO>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
    }
}
