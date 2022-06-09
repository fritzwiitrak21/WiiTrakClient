﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class TechnicianCreationDto
    {
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_]+@[a-z]+\.[a-z]{2,3}$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string Phone { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;

        public Guid SystemOwnerId { get; set; }
    }
}
