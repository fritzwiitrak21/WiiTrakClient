using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketDto
    {
        public Guid Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public long DeliveryTicketNumber { get; set; }
        [Required]
        [Range(1, 300, ErrorMessage = " ")] 
        public int NumberOfCarts { get; set; }

        public string Grid { get; set; } = string.Empty;

        public string PicUrl { get; set; } = string.Empty;

        public string SignaturePicUrl { get; set; } = string.Empty;
        
        public bool SignOffRequired { get; set; }

        public bool ApprovedByStore { get; set; }

        public string StoreName { get; set; } = string.Empty;

        public string StoreNumber { get; set; } = string.Empty;

        public string DriverName { get; set; }= string.Empty;

        public DateTime DeliveredAt { get; set; }

        public Guid ServiceProviderId { get; set; } = Guid.Empty;

        public Guid StoreId { get; set; } = Guid.Empty;

        public Guid DriverId { get; set; } = Guid.Empty;
        public string Signee { get;set; } = string.Empty;
        public int DriverNumber { get; set; }
    }
}
