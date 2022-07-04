/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using Microsoft.AspNetCore.Components;
using WiiTrakClient.DTOs;
namespace WiiTrakClient.Shared.Message
{
    public partial class MessagesList: ComponentBase
    {
        [Parameter]
        public List<MessagesDto>? Messages { get; set; } = null;
    }
}
