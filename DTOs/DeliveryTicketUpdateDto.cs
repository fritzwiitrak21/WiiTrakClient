using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketUpdateDto
    {
        public long DeliveryTicketNumber { get; set; }
        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = " ")]
        [Range(1, 300, ErrorMessage = " ")]
        public int NumberOfCarts { get; set; }

        public string Grid { get; set; } = string.Empty;

        public string PicUrl { get; set; } = string.Empty;
        [Required] 
        public string SignaturePicUrl { get; set; } = string.Empty;

        public bool SignOffRequired { get; set; }

        public bool ApprovedByStore { get; set; }

        public DateTime DeliveredAt { get; set; }

        public Guid ServiceProviderId { get; set; } = Guid.Empty;

        public Guid StoreId { get; set; } = Guid.Empty;

        public Guid DriverId { get; set; } = Guid.Empty;
        [Required]
        [StringLength(25, ErrorMessage =" ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage =" ")]
        public string Signee { get; set; } = string.Empty;
    }
}
