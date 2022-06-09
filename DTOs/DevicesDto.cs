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
        public string DeviceModel { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public string IMEI { get; set; }
        [Required]
        public string ICCID { get; set; }
        [Required]
        public string IMSI { get; set; }
        [Required]
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
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
