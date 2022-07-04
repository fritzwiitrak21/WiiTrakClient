﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class RepairIssueDto
    {
        public Guid Id { get; set; }
        public string? Issue { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
