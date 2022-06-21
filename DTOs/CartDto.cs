/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string ManufacturerName { get; set; } = string.Empty;
        [Required]
        public DateTime? DateManufactured { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9 ]*$")]
        public string CartNumber { get; set; } = string.Empty;
        [Required]
        public CartOrderedFrom OrderedFrom { get; set; }
        [Required]
        public CartCondition Condition { get; set; }
        [Required]
        public CartStatus Status { get; set; }
        public string PicUrl { get; set; } = string.Empty;
        public bool IsProvisioned { get; set; }
        public string BarCode { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public Guid? DeviceId { get; set; }
        public bool IsActive { get; set; }
        public StoreDto? Store {get; set;}
        public TrackingDeviceDto? TrackingDevice { get; set; }
        public string DamageIssue { get; set; } = string.Empty;
        public string IssueType { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
    }
}
