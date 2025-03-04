using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using FarmAd.Infrastructure.Service;
using FarmAd.Persistence.Service.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FarmAd.Persistence.Services.User
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IStorageService _storageService;
        private readonly IOTPService _oTPService;
        private readonly IRedisCacheServices _redisCacheServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;

        public UserAuthenticationService(IStorageService storageService, IOTPService oTPService, IRedisCacheServices redisCacheServices, IHttpContextAccessor httpContextAccessor, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository)
        {
            _storageService = storageService;
            _oTPService = oTPService;
            _redisCacheServices = redisCacheServices;
            _httpContextAccessor = httpContextAccessor;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
        }
        //public async Task<UserAuthentication> CheckAuthenticationAsync(string code, string phoneNumber, List<string> images)
        //{
        //    TimeSpan now = DateTime.UtcNow.AddHours(4).TimeOfDay;

        //    // Göndərilən kod və telefon nömrəsi ilə uyğun gələn aktiv qeydi gətiririk
        //    UserAuthentication authentication = await _userAuthenticationReadRepository
        //        .GetAsync(x => !x.IsDisabled && x.Code == code && x.Username == phoneNumber);

        //    if (authentication != null)
        //    {
        //        // Əgər kodun müddəti bitibsə

        //        if (authentication.ExpirationDate.TimeOfDay < now)
        //        {
        //            foreach (var image in images)
        //            {
        //                await _storageService.DeleteAsync("files\\products", image);
        //            }
        //            authentication.IsDisabled = true;
        //            await _redisCacheServices.ClearAsync("ProductVM");
        //            await _redisCacheServices.ClearAsync("ProductImageFiles");
        //            await _redisCacheServices.ClearAsync("ProductPath");
        //            await _userAuthenticationWriteRepository.SaveAsync();
        //            throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
        //        }
        //        // Kod düzgün və müddəti keçməyibsə, qeydi qaytarırıq
        //        return authentication;
        //    }
        //    else
        //    {
        //        // Kod səhv daxil edilibsə: telefon nömrəsinə görə müvafiq qeydi gətirib retry limitini yoxlayırıq
        //        UserAuthentication authByPhone = await _userAuthenticationReadRepository
        //            .GetAsync(x => !x.IsDisabled && x.Username == phoneNumber);

        //        if (authByPhone != null)
        //        {
        //            if (authByPhone.Count > 0)
        //            {
        //                authByPhone.Count -= 1;
        //            }
        //            else
        //            {
        //                foreach (var image in images)
        //                {
        //                    await _storageService.DeleteAsync("files\\products", image);
        //                }
        //                authByPhone.IsDisabled = true;
        //                await _redisCacheServices.ClearAsync("ProductVM");
        //                await _redisCacheServices.ClearAsync("ProductImageFiles");
        //                await _redisCacheServices.ClearAsync("ProductPath");
        //                await _userAuthenticationWriteRepository.SaveAsync();
        //                throw new AuthenticationCodeException("Kod yanlışdır! Təkrar etmə limitiniz bitmişdir.");
        //            }
        //            await _userAuthenticationWriteRepository.SaveAsync();
        //        }
        //        throw new AuthenticationCodeException("Kod yanlışdır!Təkrar etmə limitinin sayı:" + authByPhone.Count);
        //    }
        //}

        public async Task<bool> CheckAuthenticationAsync(string token, string enteredCode, string phoneNumber)
        {

            // Hər istifadəçi üçün unikal Redis açarı
            string redisKey = $"Auth:{phoneNumber}";

            // Redis-dən saxlanılan dəyəri alırıq (əgər müddət bitibsə, AnyCode metodu istisna atacaq)
            string storedValue = await _redisCacheServices.GetValueAsync(redisKey);
            if (string.IsNullOrEmpty(storedValue))
                await _redisCacheServices.ClearAsync(redisKey);

            // OTP servisindən istifadə edərək kod, token və retry sayını ayırırıq
            var (authToken, storedCode, attemptCount) = _oTPService.SplitRedisCode(storedValue);

            // Əgər token verilibsə və Redis-dən alınan token uyğun gəlmirsə
            if (!string.IsNullOrEmpty(token) && authToken != token)
                throw new AuthenticationCodeException("Giriş icazəniz yoxdur!");


            // Yoxlama prosesini həyata keçiririk
            bool isSuccess = await CheckLoginProcess(enteredCode, authToken, phoneNumber, storedCode, attemptCount, redisKey);

            return isSuccess;
        }

        public async Task<bool> CheckAuthenticationAsync(string token, string enteredCode, string phoneNumber, List<string> images)
        {

            // Hər istifadəçi üçün unikal Redis açarı
            string redisKey = $"Auth:{phoneNumber}";

            // Redis-dən saxlanılan dəyəri alırıq (əgər müddət bitibsə, AnyCode metodu istisna atacaq)
            string storedValue = await AnyCode(phoneNumber, redisKey, images);

            // OTP servisindən istifadə edərək kod, token və retry sayını ayırırıq
            var (authToken, storedCode, attemptCount) = _oTPService.SplitRedisCode(storedValue);

            // Əgər token verilibsə və Redis-dən alınan token uyğun gəlmirsə
            if (!string.IsNullOrEmpty(token) && authToken != token)
                throw new AuthenticationCodeException("Giriş icazəniz yoxdur!");


            //bool isSuccess = await CheckProcess(enteredCode, authToken, phoneNumber, images, storedCode, attemptCount, redisKey);

            // Yoxlama prosesini həyata keçiririk
            bool isSuccess = await CheckProductCreationProcess(enteredCode, authToken, phoneNumber, images, storedCode, attemptCount, redisKey);

            return isSuccess;
        }

        public async Task<bool> CheckAuthenticationAsync(string enteredCode, string phoneNumber, List<string> images)
        {
            return await CheckAuthenticationAsync(null, enteredCode, phoneNumber, images);
        }

        private async Task<string> AnyCode(string phoneNumber, string redisKey, List<string> images)
        {
            // Redis-dən saxlanılan dəyəri alırıq
            string storedValue = await _redisCacheServices.GetValueAsync(redisKey);
            if (string.IsNullOrEmpty(storedValue))
            {
                // Kodun müddəti bitmişdir, şəkilləri silirik və əlaqədar Redis açarlarını təmizləyirik
                foreach (var image in images)
                {
                    await _storageService.DeleteAsync("files\\products", image);
                }
                await _redisCacheServices.ClearAsync($"ProductVM:{phoneNumber}");
                await _redisCacheServices.ClearAsync($"ProductImageFiles:{phoneNumber}");
                await _redisCacheServices.ClearAsync($"ProductPath:{phoneNumber}");
                await _redisCacheServices.ClearAsync(redisKey);
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Yenidən cəhd edin.");
            }
            return storedValue;
        }
        //private async Task<bool> CheckProcess(string enteredCode, string? authToken, string phoneNumber, List<string> images, string storedCode, int attemptCount, string redisKey)
        //{
        //    bool succeed = false;
        //    string updatedValue;
        //    // Girilən kod düzgün deyil
        //    if (storedCode != enteredCode)
        //    {
        //        if (attemptCount > 1)
        //        {
        //            attemptCount--;
        //            // Yenilənmiş dəyəri Redis-ə yazırıq (format: "kod|token|retry")
        //            if (!string.IsNullOrEmpty(authToken))
        //                updatedValue = $"{authToken}|{storedCode}|{attemptCount}";
        //            else
        //                updatedValue = $"{storedCode}|{attemptCount}";

        //            await _redisCacheServices.SetValueAsync(redisKey, updatedValue);
        //            throw new AuthenticationCodeException("Kod yanlışdır! Qalan cəhd sayı: " + attemptCount);
        //        }
        //        else
        //        {
        //            // Retry sayı 0 olduqda: bütün şəkilləri sil və əlaqədar Redis açarlarını təmizləyirik
        //            foreach (var image in images)
        //            {
        //                await _storageService.DeleteAsync("files\\products", image);
        //            }
        //            await _redisCacheServices.ClearAsync($"ProductVM:{phoneNumber}");
        //            await _redisCacheServices.ClearAsync($"ProductImageFiles:{phoneNumber}");
        //            await _redisCacheServices.ClearAsync($"ProductPath:{phoneNumber}");
        //            await _redisCacheServices.ClearAsync(redisKey);
        //            throw new AuthenticationCodeException("Kod yanlışdır! Təkrar etmə limiti bitmişdir.");
        //        }
        //    }
        //    else
        //    {
        //        // Kod düzgün daxil edilibsə, Redis açarını təmizləyirik və true qaytarırıq
        //        await _redisCacheServices.ClearAsync(redisKey);
        //        return succeed = true;
        //    }
        //}

        private async Task<bool> ValidateCodeAsync(string enteredCode, string storedCode, int attemptCount, string redisKey, string? authToken = null)
        {
            if (storedCode != enteredCode)
            {
                if (attemptCount > 1)
                {
                    attemptCount--;
                    string updatedValue = !string.IsNullOrEmpty(authToken)
                        ? $"{authToken}|{storedCode}|{attemptCount}"
                        : $"{storedCode}|{attemptCount}";

                    await _redisCacheServices.SetValueAsync(redisKey, updatedValue);
                    throw new AuthenticationCodeException("Kod yanlışdır! Qalan cəhd sayı: " + attemptCount);
                }
                return false;
            }

            await _redisCacheServices.ClearAsync(redisKey);
            return true;
        }

        private async Task<bool> CheckLoginProcess(string enteredCode, string? authToken, string phoneNumber, string storedCode, int attemptCount, string redisKey)
        {
            bool isValid = await ValidateCodeAsync(enteredCode, storedCode, attemptCount, redisKey, authToken);

            if (!isValid)
                throw new AuthenticationCodeException("Kod yanlışdır! Təkrar etmə limiti bitmişdir.");

            return true;
        }
        private async Task<bool> CheckProductCreationProcess(string enteredCode, string? authToken, string phoneNumber, List<string> images, string storedCode, int attemptCount, string redisKey)
        {
            bool isValid = await ValidateCodeAsync(enteredCode, storedCode, attemptCount, redisKey, authToken);

            if (!isValid)
            {
                // Əgər son cəhd bitibsə, şəkilləri sil və Redis açarlarını təmizlə
                foreach (var image in images)
                {
                    await _storageService.DeleteAsync("files\\products", image);
                }
                await _redisCacheServices.ClearAsync($"ProductVM:{phoneNumber}");
                await _redisCacheServices.ClearAsync($"ProductImageFiles:{phoneNumber}");
                await _redisCacheServices.ClearAsync($"ProductPath:{phoneNumber}");
                await _redisCacheServices.ClearAsync(redisKey);

                throw new AuthenticationCodeException("Kod yanlışdır! Təkrar etmə limiti bitmişdir.");
            }

            return true;
        }

    }
}
