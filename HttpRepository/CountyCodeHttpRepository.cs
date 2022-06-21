/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
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
            
            return response.Response;
        }
    }
}
