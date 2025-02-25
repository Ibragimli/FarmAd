using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProfileLoginServices
    {
        public Task UserLogin(string phoneNumber, string code, UserAuthentication authentication); 
        public Task UserResetPassword(string phoneNumber, string code); 
        public Task<UserAuthentication> LoginAuthentication(string code, string phoneNumber, string token);

    }
    
}
