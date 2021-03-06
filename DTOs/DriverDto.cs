/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;
namespace WiiTrakClient.DTOs
{
    public class DriverDto
    {
        public Guid Id { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(Numbers.Twenty)]
        [RegularExpression(@"^[a-zA-Z!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/? ]*$")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.Twenty)]
        [RegularExpression(@"^[a-zA-Z!@#$%^&*()_+\-=\[\]{};':\\|,.<>\/? ]*$")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.Fifty)]
        public string StreetAddress1 { get; set; } = string.Empty;
        [StringLength(Numbers.Fifty)]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.Fifteen)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [MinLength(Numbers.Four)]
        [RegularExpression(@"^[0-9-]*$")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePic { get; set; } = string.Empty;
        public bool IsSuspended { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }
        public int DriverNumber { get; set; }
    }
}
