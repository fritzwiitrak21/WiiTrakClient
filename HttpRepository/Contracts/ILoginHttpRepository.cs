﻿/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface ILoginHttpRepository
    {
        Task<UserDto> GetUsersDetailsByLoginAsync(LoginDto login);

        Task<UserDto> GetUsersDetailsByUserNameAsync(ForgotPasswordDto forgot);

        Task UpdatePasswordAsync(Guid id, ResetPasswordDto reset);
    }
}
