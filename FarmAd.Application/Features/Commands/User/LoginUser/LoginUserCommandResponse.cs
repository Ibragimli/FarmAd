﻿using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;

namespace FarmAd.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public bool Succes { get; set; }
    }
}
