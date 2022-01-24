using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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