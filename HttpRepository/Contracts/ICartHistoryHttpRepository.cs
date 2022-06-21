/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICartHistoryHttpRepository
    {
        Task<CartHistoryDto> GetCartHistoryByIdAsync(Guid id);
        Task<List<CartHistoryDto>> GetAllCartHistoryAsync();
        Task<List<CartHistoryDto>> GetCartHistoryByCartIdAsync(Guid cartId);
        Task CreateCartHistoryAsync(CartHistoryCreationDto cart);
        Task UpdateCartHistoryAsync(Guid id, CartHistoryUpdateDto cart);
        Task DeleteCartHistoryAsync(Guid id);
    }
}
