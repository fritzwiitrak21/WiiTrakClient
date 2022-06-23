/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DevicesDto
    {
        public Guid Id { get; set; }
        [Required]
        public string DeviceModel { get; set; } = string.Empty;
        [Required]
        public string DeviceName { get; set; } = string.Empty;
        [Required]
        public string IMEI { get; set; } = string.Empty;
        [Required]
        public string ICCID { get; set; } = string.Empty;
        [Required]
        public string IMSI { get; set; } = string.Empty;
        [Required]
        public Guid SIMCardId { get; set; }
        [Required]
        public string SIMNo { get; set; } = string.Empty;
        [Required]
        public DateTime? ActivatedTime { get; set; }
        [Required]
        public DateTime? SubscriptionExpiration { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMapped { get; set; }
    }
}
