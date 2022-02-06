using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WiiTrakClient.HttpRepository.Contracts
{    public interface IPicUploadHttpRepository
    {
        Task<string> UploadImage(MultipartFormDataContent content);
        Task<string> UploadSignature(MultipartFormDataContent content);
    }
}