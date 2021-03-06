/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.ComponentModel.DataAnnotations;
using WiiTrakClient.Cores;
namespace WiiTrakClient.DTOs
{
    public class DeliveryTicketUpdateDto
    {
        public long DeliveryTicketNumber { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$")]
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
        [StringLength(Numbers.TwentyFive, ErrorMessage =" ")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage =" ")]
        public string Signee { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public Guid? UpdatedBy { get; set; }
        public List<CartDto>? PickedUpCarts { get; set; }
    }
}
