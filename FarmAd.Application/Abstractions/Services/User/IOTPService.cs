using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IOTPService
    {
        public string CreateToken();
        public string CodeCreate();
        public string encryptSha256(string randomString);
        public Task<UserAuthentication> CreateAuthentication( string code, string phoneNumber);

    }
}
