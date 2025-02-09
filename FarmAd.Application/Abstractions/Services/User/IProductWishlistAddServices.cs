using FarmAd.Application.DTOs.User;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductWishlistAddServices
    {
        Task IsProduct(int id);
        Task<WishProductCreateDto> UserAddWish(int id, AppUser user);
        void CookieAddWish(int id);
        Task<AppUser> IsAuthenticated();
    }
}
