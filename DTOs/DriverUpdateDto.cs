/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DriverUpdateDto
    {
        [Required(ErrorMessage = " ")]
        [StringLength(10, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = " ")]
        [StringLength(10, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z0-9_]+@[a-z]+\.[a-z]{2,3}$")]
        public string Email { get; set; } = string.Empty;



        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = " ")]
        [StringLength(50, ErrorMessage = " ")]
        public string StreetAddress1 { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = " ")]
        public string StreetAddress2 { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [StringLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string City { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [StringLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string State { get; set; } = string.Empty;


        public string CountryCode { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [MinLength(4)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = " ")]
        public string PostalCode { get; set; } = string.Empty;


        public string ProfilePic { get; set; } = string.Empty;
        public bool IsSuspended { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }
        public int DriverNumber { get; set; }

    }
}
