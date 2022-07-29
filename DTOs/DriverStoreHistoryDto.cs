﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class DriverStoreHistoryDto
    {
        public string StoreName { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string AssignedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
