using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DriverStoreDetailsDto
    {
        public Guid Id { get; set; }//Store Id
        public DateTime CreatedAt { get; set; }
        
        public bool IsActive { get; set; }// for  Store 
        public bool DriverStoresIsActive { get; set; }// for Driverstore
        
        public string StoreName { get; set; } = string.Empty;

        public string StoreNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhonePrimary { get; set; } = string.Empty;

        public string PhoneSecondary { get; set; } = string.Empty;

        public string StreetAddress1 { get; set; } = string.Empty;

        public string StreetAddress2 { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string CountryCode { get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Invalid Zip")]
        public string PostalCode { get; set; } = string.Empty;

        public string AssignedDriver { get; set; } = string.Empty;
    }
}
