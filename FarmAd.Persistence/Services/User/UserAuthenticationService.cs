using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using FarmAd.Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Persistence.Services.User
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IStorageService _storageService;
        private readonly IRedisCacheServices _redisCacheServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;

        public UserAuthenticationService(IStorageService storageService, IRedisCacheServices redisCacheServices, IHttpContextAccessor httpContextAccessor, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository)
        {
            _storageService = storageService;
            _redisCacheServices = redisCacheServices;
            _httpContextAccessor = httpContextAccessor;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
        }
        public async Task<UserAuthentication> CheckAuthenticationAsync(string code, string phoneNumber, List<string> images)
        {
            TimeSpan now = DateTime.UtcNow.AddHours(4).TimeOfDay;

            // Göndərilən kod və telefon nömrəsi ilə uyğun gələn aktiv qeydi gətiririk
            UserAuthentication authentication = await _userAuthenticationReadRepository
                .GetAsync(x => !x.IsDisabled && x.Code == code && x.Username == phoneNumber);

            if (authentication != null)
            {
                // Əgər kodun müddəti bitibsə

                if (authentication.ExpirationDate.TimeOfDay < now)
                {
                    foreach (var image in images)
                    {
                        await _storageService.DeleteAsync("files\\products", image);
                    }
                    authentication.IsDisabled = true;
                    await _redisCacheServices.ClearAsync("ProductVM");
                    await _redisCacheServices.ClearAsync("ProductImageFiles");
                    await _userAuthenticationWriteRepository.SaveAsync();
                    throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
                }
                // Kod düzgün və müddəti keçməyibsə, qeydi qaytarırıq
                return authentication;
            }
            else
            {
                // Kod səhv daxil edilibsə: telefon nömrəsinə görə müvafiq qeydi gətirib retry limitini yoxlayırıq
                UserAuthentication authByPhone = await _userAuthenticationReadRepository
                    .GetAsync(x => !x.IsDisabled && x.Username == phoneNumber);

                if (authByPhone != null)
                {
                    if (authByPhone.Count > 0)
                    {
                        authByPhone.Count -= 1;
                    }
                    else
                    {
                        foreach (var image in images)
                        {
                            await _storageService.DeleteAsync("files\\products", image);
                        }
                        authByPhone.IsDisabled = true;
                        await _redisCacheServices.ClearAsync("ProductVM");
                        await _redisCacheServices.ClearAsync("ProductImageFiles");
                        await _redisCacheServices.ClearAsync("ProductPath");
                        await _userAuthenticationWriteRepository.SaveAsync();
                        throw new AuthenticationCodeException("Kod yanlışdır! Təkrar etmə limitiniz bitmişdir.");
                    }
                    await _userAuthenticationWriteRepository.SaveAsync();
                }
                throw new AuthenticationCodeException("Kod yanlışdır!Təkrar etmə limitinin sayı:" + authByPhone.Count);
            }
        }

        //public async Task<UserAuthentication> CheckAuthenticationAsync(string code, string phoneNumber, List<string> images)
        //{
        //    TimeSpan now = DateTime.UtcNow.AddHours(4).TimeOfDay;

        //    UserAuthentication authentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Code == code && x.Username == phoneNumber);
        //    UserAuthentication authenticationCount = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Username == phoneNumber);


        //    //Kodun müddəti bitmişdir
        //    if (authentication != null && authentication?.ExpirationDate.TimeOfDay < now)
        //    {
        //        foreach (var image in images)
        //        {
        //            await _storageService.DeleteAsync("files\\products", image);
        //        }
        //        authentication.IsDisabled = true;
        //        await _redisCacheServices.ClearAsync("ProductVM");
        //        await _redisCacheServices.ClearAsync("ProductImageFiles");
        //        throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
        //    }
        //    //Kod dogrulugunun yoxlanilmasi, təkrar yoxlama limiti
        //    if (authenticationCount != null && authentication == null)
        //    {
        //        if (authenticationCount.Count > 1)
        //            authenticationCount.Count -= 1;
        //        else
        //        {
        //            foreach (var image in images)
        //            {
        //                await _storageService.DeleteAsync("files\\products", image);
        //            }
        //            authenticationCount.IsDisabled = true;
        //            await _redisCacheServices.ClearAsync("ProductVM");
        //            await _redisCacheServices.ClearAsync("ProductImageFiles");
        //        }
        //        await _userAuthenticationWriteRepository.SaveAsync();

        //        throw new ExpirationDateException("Kod yanlışdır! Təkrar giriş edin");
        //    }

        //    return authentication;
        //}
    }
}
