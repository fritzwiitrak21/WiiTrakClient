/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;

namespace WiiTrakClient.DTOs
{
    public class CountyCodeDTO
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [MaxLength(Numbers.Twenty)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string CountyName { get; set; } = string.Empty;
        [Required]
        [MinLength(Numbers.Three)]
        [MaxLength(Numbers.Four)]
        [RegularExpression(@"^[a-zA-Z]*$")]
        public string CountyCode { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        [Required]
        [MaxLength(Numbers.Fifteen)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string State { get; set; } = string.Empty;
        [Required]
        [MaxLength(Numbers.Fifteen)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [MinLength(Numbers.Four)]
        [RegularExpression(@"^[0-9-]*$")]
        public string ZipCode { get; set; } = string.Empty;
    }
}
