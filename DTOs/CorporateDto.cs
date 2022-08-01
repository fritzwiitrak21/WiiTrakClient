/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;
namespace WiiTrakClient.DTOs
{
    public class CorporateDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(Numbers.TwentyFive)]
        [RegularExpression(@"^[a-zA-Z!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/? ]*$")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.Fifty)]
        public string StreetAddress1 { get; set; } = string.Empty;
        [StringLength(Numbers.Fifty)]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required]
        [MaxLength(Numbers.Fifteen)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(Numbers.Fifteen)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [MinLength(Numbers.Four)]
        [RegularExpression(@"^[0-9-]*$")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhonePrimary { get; set; } = string.Empty;
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhoneSecondary { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }
        public List<StoreDto>? Stores { get; set; }
    }
}
