using FarmAd.Application.Abstractions.Services.User;
using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;

        public UserAuthenticationService(IStorageService storageService, IHttpContextAccessor httpContextAccessor, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository)
        {
            _storageService = storageService;
            _httpContextAccessor = httpContextAccessor;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
        }
        public async Task<UserAuthentication> CheckAuthentication(string code, string phoneNumber, List<string> images)
        {
            TimeSpan now = DateTime.UtcNow.AddHours(4).TimeOfDay;

            UserAuthentication authentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Code == code && x.Username == phoneNumber);
            if (authentication == null)
                throw new ExpirationDateException("Kod yanlışdır! Təkrar giriş edin");

            //Kodun müddəti bitmişdir
            if (authentication.ExpirationDate.TimeOfDay < now)
            {
                foreach (var image in images)
                {
                    await _storageService.DeleteAsync(image, "Product");
                }
                authentication.IsDisabled = true;
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
                await _userAuthenticationWriteRepository.SaveAsync();
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");
            }

            //Kod dogrulugunun yoxlanilmasi, təkrar yoxlama limiti
            if (authentication == null)
            {
                if (authentication.Count > 1)
                    authentication.Count -= 1;
                else
                {
                    foreach (var image in images)
                    {
                        _storageService.DeleteAsync(image, "Product");
                    }
                    authentication.IsDisabled = true;
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                    _httpContextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
                }
                await _userAuthenticationWriteRepository.SaveAsync();

                throw new AuthenticationCodeException("Kod yanlışdır!");
            }
            return authentication;
        }
    }
}
