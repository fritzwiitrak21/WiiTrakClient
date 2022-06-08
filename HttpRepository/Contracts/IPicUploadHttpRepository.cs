/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IPicUploadHttpRepository
    {
        Task<string> UploadImage(MultipartFormDataContent content);
        Task<string> UploadSignature(MultipartFormDataContent content);
    }
}