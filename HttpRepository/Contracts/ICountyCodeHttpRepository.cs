using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ICountyCodeHttpRepository
    {
        Task<List<CountyCodeDTO>> GetCountyListAsync();
    }
}
