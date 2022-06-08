/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.Enums;

namespace WiiTrakClient.DTOs
{
    public class CartHistoryUpdateDto
    {
       public DateTime? PickedUpAt { get; set; }

        public DateTime? DroppedOffAt { get; set; }

        public DateTime? ProvisionedAt { get; set; }

        public Guid? ServiceProviderId { get; set; } = Guid.Empty;

        public Guid? StoreId { get; set; } = Guid.Empty;

        public Guid? DriverId { get; set; } = Guid.Empty;

        public CartCondition Condition { get; set; }

        public CartStatus Status { get; set; }

        public bool IsDelivered { get; set; }

        public Guid? DeliveryTicketId { get; set; } = Guid.Empty;

        public double PickupLongitude { get; set; }

        public double PickupLatitude { get; set; }

        public Guid CartId { get; set; }
        public string IssueType { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
    }
}