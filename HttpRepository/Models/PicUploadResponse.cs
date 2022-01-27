using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WiiTrakClient.HttpRepository.Models
{
    public class PicUploadResponse
    {
       [JsonPropertyName("fileURL")]
        public string FileURL {get; set;}
    }
}