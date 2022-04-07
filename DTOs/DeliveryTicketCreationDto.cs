using System.ComponentModel.DataAnnotations;

namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketCreationDto
    {
        public long DeliveryTicketNumber { get; set; }
        [Required]
        [Range(1,300, ErrorMessage =" ")]
        public int NumberOfCarts { get; set; }

        public string Grid { get; set; } = string.Empty;

        public string PicUrl { get; set; } = string.Empty;

        public string SignaturePicUrl { get; set; } = string.Empty;
        public bool SignOffRequired { get; set; }

        public DateTime DeliveredAt { get; set; }

        public Guid ServiceProviderId { get; set; } = Guid.Empty;

        public Guid StoreId { get; set; } = Guid.Empty;

        public Guid DriverId { get; set; } = Guid.Empty;
        [Required]
        [StringLength(50)]
        public string Signee { get; set; } = string.Empty;
    }
}
