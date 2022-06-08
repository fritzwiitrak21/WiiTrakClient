/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;
using System.Text.Json;
using WiiTrakClient.HttpRepository.Models;


namespace WiiTrakClient.HttpRepository
{
    public class PicUploadHttpRepository : IPicUploadHttpRepository
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "upload";
        private readonly string _apiUrl;

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

        public async Task<string> UploadSignature(MultipartFormDataContent content)
        {
            string url = $"{_apiUrl}/signature";

            var response = await _httpService.PostForm(url, content);
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