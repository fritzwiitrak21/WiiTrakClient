/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;
namespace WiiTrakClient.DTOs
{
    public class SimCardsDto
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(Numbers.TwentyFive)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string TelecomCompany { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.TwentyFive)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string PlanName { get; set; } = string.Empty;
        [Required]
        public DateTime? PlanActivationDate { get; set; }
        [Required]
        public DateTime? PlanEndDate { get; set; }
        [Required]
        [MinLength(Numbers.Four)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string SIMNo { get; set; } = string.Empty;
        [Required]
        [MinLength(Numbers.Four)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string IMSI { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMapped { get; set; }
        public Guid TechnicianId { get; set; }
    }
}
