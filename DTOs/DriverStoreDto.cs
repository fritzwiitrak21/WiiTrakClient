﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class DriverStoreDto
    {
        public Guid DriverId { get; set; }

        public Guid StoreId { get; set; }

        public DriverDto? Driver { get; set; }

        public StoreDto? Store { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
