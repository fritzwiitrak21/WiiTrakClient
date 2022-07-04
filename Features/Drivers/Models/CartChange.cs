/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.Enums;

namespace WiiTrakClient.Features.Drivers.Models
{
    public class CartChange
    {   
        public Guid Id { get; set; }
        public CartCondition Condition { get; set; }
        public CartStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DamageIssue { get; set; } = string.Empty;
    }
}
