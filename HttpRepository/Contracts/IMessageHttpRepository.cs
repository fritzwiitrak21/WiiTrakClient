/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;
using WiiTrakClient.Enums;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IMessageHttpRepository
    {
        Task<List<MessagesDto>> GetMessagesByIdAsync(Guid Id, Role Role);
        Task AddNewMessageAsync(MessagesDto NewMessage);
        Task UpdateOldMessageAsync(Guid Id, MessagesDto NewMessage);
        Task UpdateMessageDeliveredTimeAsync();
        Task UpdateMessageActionAsync(MessagesDto dto);
    }
}
