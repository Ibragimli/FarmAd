using FarmAd.Application.DTOs;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IOTPService
    {
        Task CreateAuthenticationAsync(string username, string code);
        Task CreateAuthenticationAsync(string token, string username, string code);
        public string CodeCreate();
        (string, string, int) SplitRedisCode(string storedValue);
        public Task<UserAuthentication> CreateAuthentication(string code, string phoneNumber);

    }
}
