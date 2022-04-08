using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DriverCreationDto
    {
        [Required(ErrorMessage = " ")]
        [StringLength(10, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [StringLength(10, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[a-z0-9]+@[a-z]+\.[a-z]{2,3}$", ErrorMessage = " ")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$", ErrorMessage = " ")]
        public string Phone { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [StringLength(25, ErrorMessage = " ")]
        public string StreetAddress1 { get; set; } = string.Empty;


        [Required(ErrorMessage = " ")]
        [StringLength(25, ErrorMessage = " ")]
        public string StreetAddress2 { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [StringLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [StringLength(15, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string State { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        [Required(ErrorMessage = " ")]
        [MinLength(4)]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = " ")]
        public string PostalCode { get; set; } = string.Empty;

        public string ProfilePic { get; set; } = string.Empty;
        public bool IsSuspended { get; set; }
        public bool IsActive { get; set; }

        public Guid CompanyId { get; set; }
        public Guid SystemOwnerId { get; set; }
        public int DriverNumber { get; set; }



    }
}
