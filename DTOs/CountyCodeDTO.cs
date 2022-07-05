/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class CountyCodeDTO
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string CountyName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        [MaxLength(4)]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string CountyCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string State { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[1-9][0-9]*$")]
        public string ZipCode { get; set; } = string.Empty;
    }
}
