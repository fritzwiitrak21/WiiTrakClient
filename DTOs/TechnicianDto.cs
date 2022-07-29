/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;
namespace WiiTrakClient.DTOs
{
    public class TechnicianDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(Numbers.Ten)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [StringLength(Numbers.Ten)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string Phone { get; set; } = string.Empty;
        public string ProfilePic { get; set; } = string.Empty;
        public Guid SystemOwnerId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
