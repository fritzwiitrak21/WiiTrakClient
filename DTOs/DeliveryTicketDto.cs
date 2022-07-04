/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public long DeliveryTicketNumber { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public int NumberOfCarts { get; set; }
        public string Grid { get; set; } = string.Empty;
        public string PicUrl { get; set; } = string.Empty;
        public string SignaturePicUrl { get; set; } = string.Empty;
        public bool SignOffRequired { get; set; }
        public bool ApprovedByStore { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string StoreNumber { get; set; } = string.Empty;
        public string DriverName { get; set; }= string.Empty;
        public DateTime DeliveredAt { get; set; }
        public Guid ServiceProviderId { get; set; } = Guid.Empty;
        public Guid StoreId { get; set; } = Guid.Empty;
        public Guid DriverId { get; set; } = Guid.Empty;
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Signee { get;set; } = string.Empty;
        public int DriverNumber { get; set; }
        public string StreetAddress1 { get; set; } = string.Empty;
        public string StreetAddress2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool DriverStoresIsActive { get; set; }
        public bool StoresIsActive { get; set; }
        public string TimezoneDiff { get; set; } = string.Empty;
        public string TimezoneName { get; set; } = string.Empty;
        public DateTime? TimezoneDateTime { get; set; }
        public string ServiceFrequency { get; set; } = string.Empty;
    }
}
