/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;

namespace WiiTrakClient.Features.Drivers.Models
{
    public class DriverSummary
    {
        public int CartsOut { get; set; }
        public int CartsOnTruck { get; set; }
        public int CartsDelivered { get; set; }
        public int CartsLost { get; set; }
        public int CartsNeedRepair { get; set; }
        public int CartsTrash { get; set; }
        public int CartsGood { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }        
    }
}