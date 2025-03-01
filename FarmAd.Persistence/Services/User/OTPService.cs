using FarmAd.Domain.Entities;
using FarmAd.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.DTOs;

namespace FarmAd.Persistence.Service.User
{
    public class OTPService : IOTPService
    {
        private readonly IRedisCacheServices _redisCacheServices;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;

        public OTPService(IRedisCacheServices redisCacheServices, IUserAuthenticationReadRepository userAuthenticationReadRepository, IUserAuthenticationWriteRepository userAuthenticationWriteRepository)
        {
            _redisCacheServices = redisCacheServices;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
        }

        //6 reqemli OTP kodun yaradilmasi
        public string CodeCreate()
        {
            Random random = new Random();
            string code = random.Next(100001, 999999).ToString();
            return code;
        }

        //Redis Authentication Create
        public async Task CreateAuthenticationAsync(string username, string code)
        {
            string redisKey = $"Auth:{username}";
            string value = $"{code}|3";
            await _redisCacheServices.SetValueAsync(redisKey, value);
        }

        //Redis Authentication Create
        public async Task CreateAuthenticationAsync(string token, string username, string code)
        {
            string redisKey = $"Auth:{username}";
            // Token, kod və retry limitini bir araya gətiririk (məsələn: "token|kod|3")
            string value = $"{token}|{code}|3";
            await _redisCacheServices.SetValueAsync(redisKey, value);
        }

        //DB Authentication Create
        public async Task<UserAuthentication> CreateAuthentication(string code, string username)
        {
            // RareLimitCheck
            await RareLimit(username);

            // DisabledOldAuthentication 
            await DisableOldAuthentications(username);

            UserAuthentication authentication = new UserAuthentication
            {
                Code = code,
                IsDisabled = false,
                Username = username,
                Count = 3,
                CreatedDate = DateTime.UtcNow.AddHours(4)
            };

            await _userAuthenticationWriteRepository.AddAsync(authentication);
            await _userAuthenticationWriteRepository.SaveAsync();

            return authentication;
        }

        public (string, string, int) SplitRedisCode(string storedValue)
        {
            if (string.IsNullOrEmpty(storedValue))
                throw new ArgumentException("Verilən dəyər boş və ya null ola bilməz.");

            var parts = storedValue.Split('|');

            if (parts.Length == 2)
            {
                // Format: "kod|retry", token hissəsi yoxdur, ona görə boş string qaytarırıq.
                string storedCode = parts[0];
                if (!int.TryParse(parts[1], out int attemptCount))
                    throw new FormatException("Cəhd sayı düzgün formatda deyil.");
                return (storedCode, string.Empty, attemptCount);
            }
            else if (parts.Length == 3)
            {
                // Format: "kod|token|retry"
                string token = parts[0];
                string storedCode = parts[1];
                if (!int.TryParse(parts[2], out int attemptCount))
                    throw new FormatException("Cəhd sayı düzgün formatda deyil.");
                return (token, storedCode, attemptCount);
            }
            else
                throw new FormatException("Verilən məlumat düzgün formatda deyil.");
        }
        private async Task DisableOldAuthentications(string username)
        {
            var oldAuthentications = await _userAuthenticationReadRepository.GetAllAsync(x => !x.IsDisabled && x.Username == username);

            if (oldAuthentications.Any())
                foreach (var item in oldAuthentications)
                    item.IsDisabled = true;
        }
        private async Task RareLimit(string username)
        {
            var timeLimit = DateTime.UtcNow.AddHours(4).AddMinutes(-5); // Şu andan 5 dakika öncesine kadar olanları al
            var count = await _userAuthenticationReadRepository.GetTotalCountAsync(
                x => x.CreatedDate >= timeLimit && x.Username == username // Sadece belirli username'i kontrol et
            );

            if (count >= 5) // 5 veya daha fazla giriş denemesi olduysa hata fırlat
                throw new RareLimitException("5 dəqiqə ərzində bu nömrə ilə çox cəhd etdiniz! Biraz gözləyin.");
        }
        private string CreateToken()
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            var random = new Random();
            var token = new string(Enumerable.Repeat(chars, 30).Select(s => s[random.Next(s.Length)]).ToArray());
            return token;
        }
        private string encryptSha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
