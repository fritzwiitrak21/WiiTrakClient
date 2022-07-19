/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/? ]*$")]
        public string StoreName { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[A-Za-z0-9]*$")]
        public string StoreNumber { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhonePrimary { get; set; } = string.Empty;
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhoneSecondary { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string StreetAddress1 { get; set; } = string.Empty;
        [StringLength(25)]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[0-9-]*$")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(-?\d+(\.\d+)?)$")]
        public double Longitude { get; set; }
        [Required]
        [RegularExpression(@"^(-?\d+(\.\d+)?)$")]
        public double Latitude { get; set; }
        public Guid ServiceProviderId { get; set; } = Guid.Empty;
        public bool IsSignatureRequired { get; set; }
        public Guid CompanyId { get; set; }
        public Guid CorporateId { get; set; }
        public List<CartDto>? Carts { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string CountyCode { get; set; } = string.Empty;
        [Required]
        public string ServiceFrequency { get; set; } = string.Empty;
        [Required]
        public DateTime? StartDate { get; set; }
        public int Distance { get; set; }
        public bool DriverStoresIsActive { get; set; }
        public string? TimezoneDiff { get; set; } = string.Empty;
        public string? TimezoneName { get; set; } = string.Empty;
        public bool IsConnectedStore { get; set; }
    }
}
