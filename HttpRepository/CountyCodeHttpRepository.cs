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
        private readonly IHttpService Httpservice;
        private const string ControllerName = "countycode";
        private readonly string ApiUrl;
        public CountyCodeHttpRepository(IHttpService httpservice)
        {
            Httpservice = httpservice;
            ApiUrl = $"{ httpservice.BaseUrl }{ ControllerName }";
        }
        public async Task<List<CountyCodeDTO>> GetCountyListAsync()
        {
            var response = await Httpservice.Get<List<CountyCodeDTO>>(ApiUrl);
            return response.Response;
        }
        public async Task<CountyCodeDTO> GetCountyCodeByIdAsync(Guid id)
        {
            string url = $"{ApiUrl}/{id}";
            var response = await Httpservice.Get<CountyCodeDTO>(url);
            return response.Response;
        }
        public async Task CreateCountyCodeAsync(CountyCodeDTO CountyCreation)
        {
            await Httpservice.Post(ApiUrl, CountyCreation);
        }
        public async Task UpdateCartAsync(Guid id, CountyCodeDTO CountyUpdation)
        {
            await Httpservice.Put($"{ ApiUrl }/{ id }", CountyUpdation);
        }
    }
}
