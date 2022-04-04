﻿using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DriverUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePic { get; set; } = string.Empty;
        public bool IsSuspended { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
        public int DriverNumber { get; set; }

    }
}
