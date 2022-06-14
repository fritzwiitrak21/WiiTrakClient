using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class CorporateCreationDto
    {
        [Required(ErrorMessage = " ")]
        [StringLength(25, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [StringLength(50, ErrorMessage = " ")]
        public string StreetAddress1 { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = " ")]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [MaxLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [MaxLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [MinLength(4, ErrorMessage = " ")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = " ")]
        public string PostalCode { get; set; } = string.Empty;
        public string ProfilePicUrl { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]{2,3}$", ErrorMessage = " ")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string PhonePrimary { get; set; } = string.Empty;
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string PhoneSecondary { get; set; } = string.Empty;
        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }
    }
}
