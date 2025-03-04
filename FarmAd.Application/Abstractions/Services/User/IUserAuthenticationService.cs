using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IUserAuthenticationService
    {
        Task<bool> CheckAuthenticationAsync(string code, string phoneNumber, List<string> images);
        Task<bool> CheckAuthenticationAsync(string token, string code, string phoneNumber, List<string> images);
        Task<bool> CheckAuthenticationAsync(string token, string code, string phoneNumber);
    }
}
