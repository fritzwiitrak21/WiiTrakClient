/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICartHttpRepository
    {
        Task<List<CartDto>> GetAllCartsAsync();

        Task<List<CartDto>> GetCartsByDeliveryTicketIdAsync(Guid deliveryTicketId);

        Task<List<CartDto>> GetCartsByStoreIdAsync(Guid storeId);

        Task<List<CartDto>> GetCartsByDriverIdAsync(Guid driverId);

        Task<List<CartDto>> GetCartsByCorporateIdAsync(Guid corporateId);

        Task<List<CartDto>> GetCartsByCompanyIdAsync(Guid companyId);

        Task<CartDto> GetCartByIdAsync(Guid id);

        Task CreateCartAsync(CartCreationDto cart);

        Task UpdateCartAsync(Guid id, CartUpdateDto cart);

        Task DeleteCartAsync(Guid id);
    }
}
