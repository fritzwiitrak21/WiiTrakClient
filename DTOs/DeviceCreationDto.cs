using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DeviceCreationDto
    {
        [Required]
        public string DeviceModel { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string IMEI { get; set; }
        [Required]
        public string ICCID { get; set; }
        [Required]
        public string IMSI { get; set; }
        public Guid SIMCardId { get; set; }
        [Required]
        public string SIMNo { get; set; }
        [Required]
        public DateTime? ActivatedTime { get; set; }
        [Required]
        public DateTime? SubscriptionExpiration { get; set; }
        [Required]
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
