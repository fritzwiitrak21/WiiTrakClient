﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class MessagesDto
    {
        public Guid Id { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid SenderId { get; set; }
        public int SenderRoleId { get; set; }
        public Guid StoreId { get; set; }
        public string Store { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public Guid ReciverId { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public bool IsActionTaken { get; set; }
        public string ActionTaken { get; set; } = string.Empty;
        public DateTime? ActionTakenAt { get; set; }
        public string ReciverName { get; set; } = string.Empty;
    }
}

