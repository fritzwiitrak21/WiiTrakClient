﻿using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class CartCreationDto
    {
        public string ManufacturerName { get; set; } = string.Empty;

        public DateTime DateManufactured { get; set; }

        public AssetOrderedFrom OrderedFrom { get; set; }

        public AssetCondition Condition { get; set; }

        public AssetStatus Status { get; set; }

        public string PicUrl { get; set; } = string.Empty;

        public bool IsProvisioned { get; set; }

        public string BarCode { get; set; } = string.Empty;

        public Guid StoreId { get; set; }

        public TrackingDeviceDto? TrackingDevice { get; set; }
    }
}
