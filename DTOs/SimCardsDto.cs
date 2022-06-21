/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
namespace WiiTrakClient.DTOs
{
    public class SimCardsDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string TelecomCompany { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string PlanName { get; set; } = string.Empty;
        [Required]
        public DateTime? PlanActivationDate { get; set; }
        [Required]
        public DateTime? PlanEndDate { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string SIMNo { get; set; } = string.Empty;
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string IMSI { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
