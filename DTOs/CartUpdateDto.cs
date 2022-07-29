﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Enums;
using WiiTrakClient.Cores;

namespace WiiTrakClient.DTOs
{
    public class CartUpdateDto
    {
        [Required]
        [StringLength(Numbers.TwentyFive)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string ManufacturerName { get; set; } = string.Empty;
        [Required]
        public DateTime? DateManufactured { get; set; }
        [Required]
        [RegularExpression(@"^[1-9][0-9 ]*$")]
        public string CartNumber { get; set; } = string.Empty;
        public CartOrderedFrom OrderedFrom { get; set; }
        public CartCondition Condition { get; set; }
        public CartStatus Status { get; set; }
        public string PicUrl { get; set; } = string.Empty;
        public bool IsProvisioned { get; set; }
        public string BarCode { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public Guid DeviceId { get; set; }
        public bool IsActive { get; set; }
        public CartHistoryUpdateDto CartHistory { get; set; }
        public string IssueType { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public Guid? CreatedBy { get; set; }
    }
}
