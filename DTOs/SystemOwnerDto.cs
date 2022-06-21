/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class SystemOwnerDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StreetAddress1 { get; set; } = string.Empty;
        public string StreetAddress2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhonePrimary { get; set; } = string.Empty;
        public string PhoneSecondary { get; set; } = string.Empty;
        public List<CompanyDto>? Customers { get; set; }
        public List<TechnicianDto>? Technicians { get; set; }
        public List<TrackingDeviceDto>? TrackingDevices { get; set; }
    }
}
