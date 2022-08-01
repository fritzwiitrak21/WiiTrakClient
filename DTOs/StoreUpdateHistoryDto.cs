/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class StoreUpdateHistoryDto
    {
        public string StoreName { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsNew { get; set; }
        public string Action { get; set; } = string.Empty;
    }
}
