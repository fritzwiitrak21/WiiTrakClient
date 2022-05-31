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
