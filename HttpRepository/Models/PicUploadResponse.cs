using System.Text.Json.Serialization;

namespace WiiTrakClient.HttpRepository.Models
{
    public class PicUploadResponse
    {
       [JsonPropertyName("fileURL")]
        public string FileURL {get; set;}
    }
}