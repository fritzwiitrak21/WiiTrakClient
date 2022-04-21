﻿using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class StoreDto
    {
        public Guid Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public string StoreName { get; set; } = string.Empty;

        public string StoreNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhonePrimary { get; set; } = string.Empty;

        public string PhoneSecondary { get; set; } = string.Empty;

        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Guid ServiceProviderId { get; set; }
        public bool IsSignatureRequired { get; set; }

        public Guid CompanyId { get; set; }

        public Guid CorporateId { get; set; }
        

         public List<CartDto>? Carts { get; set; }

        public bool IsActive { get; set; }

    }
}
