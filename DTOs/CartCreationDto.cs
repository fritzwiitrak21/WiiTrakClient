using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class CartCreationDto
    {
        [Required]
        [StringLength(25)]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string ManufacturerName { get; set; } = string.Empty;
        [Required]
        public DateTime? DateManufactured { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
        public string CartNumber { get; set; } = string.Empty;

        public CartOrderedFrom OrderedFrom { get; set; }

        public CartCondition Condition { get; set; }

        public CartStatus Status { get; set; }

        public string PicUrl { get; set; } = string.Empty;

        public bool IsProvisioned { get; set; }

        public string BarCode { get; set; } = string.Empty;

        public Guid StoreId { get; set; }
        public Guid DeviceId { get; set; }
        public bool IsActive { get; set; }

        public TrackingDeviceDto? TrackingDevice { get; set; }
    }
}
