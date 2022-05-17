﻿

using System.ComponentModel.DataAnnotations;

namespace  WiiTrakClient.DTOs
{
    public class CorporateDto
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
        
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
        
        public string PhonePrimary { get; set; } = string.Empty;
        
        public string PhoneSecondary { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }

        public List<StoreDto> Stores { get; set; }
    }
}
