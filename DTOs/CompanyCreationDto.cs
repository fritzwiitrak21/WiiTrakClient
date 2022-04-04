using System.ComponentModel.DataAnnotations;
namespace WiiTrakClient.DTOs
{
    public class CompanyCreationDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required] 
        [MaxLength(50)]
        public string StreetAddress1 { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string State { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhonePrimary { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneSecondary { get; set; } = string.Empty;

        public Guid? ParentId { get; set; } = null;

        public bool IsInactive { get; set; }

        public bool CannotHaveChildren { get; set; }

        public Guid SystemOwnerId { get; set; }

        public Guid CorparateId { get; set; }

        public List<ServiceProviderDto>? ServiceProviders { get; set; }

        public List<DriverDto>? Drivers { get; set; }
    }
}
