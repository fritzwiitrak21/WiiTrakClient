/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.Collections.Generic;
using WiiTrakClient.DTOs;
using System.Threading.Tasks;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICountyCodeHttpRepository
    {
        Task<List<CountyCodeDTO>> GetCountyListAsync();
    }
}
