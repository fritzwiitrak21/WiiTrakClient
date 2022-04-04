using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class CorporateUpdateDto
    {
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string StreetAddress1 { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string State { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhonePrimary { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneSecondary { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
    }
}
