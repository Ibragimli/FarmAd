using FarmAd.Application.DTOs.User;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{

    public interface IProductWishlistDeleteServices
    {
        Task IsProduct(int id);
        Task<WishItem> UserDeleteWish(int id, AppUser user);
        List<WishItemDto> CookieDeleteWish(int id, List<WishItemDto> wishItems);
        Task<AppUser> IsAuthenticated();
    }
}
