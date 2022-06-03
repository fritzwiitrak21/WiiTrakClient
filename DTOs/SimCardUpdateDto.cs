using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class SimCardUpdateDto
    {
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string TelecomCompany { get; set; }
        [Required]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string PlanName { get; set; }
        [Required]
        public DateTime? PlanActivationDate { get; set; }
        [Required]
        public DateTime? PlanEndDate { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string SIMNo { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[0-9 ]*$")]
        public string IMSI { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
