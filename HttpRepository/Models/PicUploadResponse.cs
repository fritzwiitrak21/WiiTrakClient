/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.Text.Json.Serialization;

namespace WiiTrakClient.HttpRepository.Models
{
    public class PicUploadResponse
    {
       [JsonPropertyName("fileURL")]
        public string FileURL {get; set;}
    }
}