/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using WiiTrakClient.Helpers;

namespace WiiTrakClient.Services
{
    public interface IHttpService
    {
        string BaseUrl { get; }
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        Task<HttpResponseWrapper<object>> PostForm(string url, MultipartFormDataContent content);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T data);
        Task<HttpResponseWrapper<object>> Patch(string url, string data);
        Task<HttpResponseWrapper<object>> Delete(string url);

        //Task<string> AuthenticateAsync(LoginDto login);
    }
}
