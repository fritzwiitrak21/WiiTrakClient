using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICartHttpRepository
    {
        Task<List<CartDto>> GetAllCartsAsync();

        Task<List<CartDto>> GetCartsByDriverIdAsync(Guid driverId);

        Task<CartDto> GetCartByIdAsync(Guid id);

        Task CreateCartAsync(CartCreationDto cart);

        Task UpdateCartAsync(Guid id, CartUpdateDto cart);

        Task DeleteCartAsync(Guid id);
    }
}
