using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketCreationDto
    {
        public long DeliveryTicketNumber { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage =" ")]
        public int NumberOfCarts { get; set; }

        public string Grid { get; set; } = string.Empty;
        [Required(ErrorMessage =" ")]
        public string PicUrl { get; set; } = string.Empty;

        public string SignaturePicUrl { get; set; } = string.Empty;
        public bool SignOffRequired { get; set; }

        public DateTime DeliveredAt { get; set; }

        public Guid ServiceProviderId { get; set; } = Guid.Empty;

        public Guid StoreId { get; set; } = Guid.Empty;

        public Guid DriverId { get; set; } = Guid.Empty;
        [Required]
        [StringLength(25, ErrorMessage = " ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = " ")]
        public string Signee { get; set; } = string.Empty;
    }
}
