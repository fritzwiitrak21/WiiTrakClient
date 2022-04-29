using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class StoreCreationDto
    {
        [Required(ErrorMessage = " ")]
        [StringLength(25, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = " ")]
        public string StoreName { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [StringLength(10, ErrorMessage = " ")]
        [RegularExpression(@"^[A-Za-z0-9]*$", ErrorMessage = " ")]
        public string StoreNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]{2,3}$", ErrorMessage = " ")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string PhonePrimary { get; set; } = string.Empty;


        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string PhoneSecondary { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [StringLength(25, ErrorMessage = " ")]
        public string StreetAddress1 { get; set; } = string.Empty;


        [StringLength(25, ErrorMessage = " ")]
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
        [MinLength(4)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = " ")]
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePicUrl { get; set; } = string.Empty;


        [Required]
        [RegularExpression(@"^(-?\d+(\.\d+)?)$")]
        public double Longitude { get; set; }


        [Required]
        [RegularExpression(@"^(-?\d+(\.\d+)?)$")]
        public double Latitude { get; set; }

        public Guid ServiceProviderId { get; set; }
        public string CountyCode { get; set; } = string.Empty;
        public string ServiceFrequency { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public Guid CorporateId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsSignatureRequired { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string CountyCode { get; set; } = string.Empty;
        [Required]
        public string ServiceFrequency { get; set; } = string.Empty;
        [Required]
        public DateTime? StartDate { get; set; }
    }
}
