/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICountyCodeHttpRepository
    {
        Task<List<CountyCodeDTO>> GetCountyListAsync();
        Task<CountyCodeDTO> GetCountyCodeByIdAsync(Guid id);
        Task CreateCountyCodeAsync(CountyCodeDTO CountyCreation);
        Task UpdateCartAsync(Guid id, CountyCodeDTO CountyUpdation);
    }
}
