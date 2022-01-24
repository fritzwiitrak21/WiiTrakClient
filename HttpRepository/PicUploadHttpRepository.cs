using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using WiiTrakClient.HttpRepository.Models;

namespace WiiTrakClient.HttpRepository
{
    public class PicUploadHttpRepository: IPicUploadHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "upload";
        private readonly string _apiUrl = "";

        public PicUploadHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<string> UploadImage(MultipartFormDataContent content)
        {
            var response = await _httpService.PostForm(_apiUrl, content);
            string jsonString = await response.GetBody();
            if (!response.Success)
            {
                throw new ApplicationException(jsonString);
            }

            PicUploadResponse picUploadResponse = JsonSerializer.Deserialize<PicUploadResponse>(jsonString);            
            return picUploadResponse is not null ? picUploadResponse.FileURL : string.Empty;
        }
    }
}