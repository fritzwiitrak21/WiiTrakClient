using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;
using WiiTrakClient.HttpRepository.Contracts;
using WiiTrakClient.Services;

namespace WiiTrakClient.HttpRepository
{
    public class LoginHttpRepository : ILoginHttpRepository 
    {
        private readonly IHttpService _httpService;
        private const string ControllerName = "login"; 
        private readonly string _apiUrl = "";
       

        public LoginHttpRepository(IHttpService httpService)
        {
            _httpService = httpService;
            _apiUrl = $"{ httpService.BaseUrl }{ ControllerName }";
        }

        public async Task<List<UserDto>> GetUserLoginDetailsAsync()
        {
            var response = await _httpService.Get<List<UserDto>>(_apiUrl);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            } 
            return response.Response;
        }

        public async Task<UserDto> GetUsersDetailsByLoginAsync(LoginDto login)
        {
            var Url = $"{_apiUrl}/{login.Username}/{login.Password}";
            var response = await _httpService.Get<UserDto>(Url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return  response.Response;
        }

        public async Task<UserDto> GetUsersDetailsByUserNameAsync(ForgotPasswordDto forgot)
        {
            var Url = $"{_apiUrl}/{forgot.Username}";
            var response = await _httpService.Get<UserDto>(Url);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task UpdatePasswordAsync(Guid id, ResetPasswordDto reset)
        {

            var response = await _httpService.Put($"{ _apiUrl }/{ id }", reset);
            if (!response.Success)
            {
                // throw new ApplicationException(await response.GetBody()); 
            } 
        }
        //public async Task ChangeUserPasswordAsync(Guid id,ChangePasswordDto change)
        //{
        //    var response = await _httpService.Put($"{ _apiUrl }/{ id }", change);
        //    if(!response.Success)
        //    {
        //        // throw new ApplicationException(await response.GetBody()); 
        //    }
        //}

       
    }
}
