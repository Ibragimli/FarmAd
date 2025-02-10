using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.WishItem;

namespace FarmAd.Persistence.Services.User
{
    public class ProductWishlistDeleteServices : IProductWishlistDeleteServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWishItemWriteRepository _wishItemWriteRepository;
        private readonly IWishItemReadRepository _wishItemReadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public ProductWishlistDeleteServices(IProductReadRepository productReadRepository, IWishItemWriteRepository wishItemWriteRepository, IWishItemReadRepository wishItemReadRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager) : base()
        {
            _productReadRepository = productReadRepository;
            _wishItemWriteRepository = wishItemWriteRepository;
            _wishItemReadRepository = wishItemReadRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public List<WishItemDto> CookieDeleteWish(int id, List<WishItemDto> wishItems)
        {
            string wish = _httpContextAccessor.HttpContext.Request.Cookies["wishItemList"];
            wishItems = JsonConvert.DeserializeObject<List<WishItemDto>>(wish);
            WishItemDto ProductWish = wishItems.Find(x => x.ProductId == id);
            if (ProductWish == null) throw new ItemNotFoundException("Mehsul Tapilmadi");

            wishItems.Remove(ProductWish);

            return wishItems;
        }

        public async Task<AppUser> IsAuthenticated()
        {
            AppUser user = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            return user;
        }
        public async Task IsProduct(int id)
        {
            if (!await _productReadRepository.IsExistAsync(x => x.Id == id))
                throw new ItemNotFoundException("Mehsul Tapilmadi");
        }

        public async Task<WishItem> UserDeleteWish(int id, AppUser user)
        {
            WishItem wishItem = await _wishItemReadRepository.GetAsync(x => x.ProductId == id);
            if (wishItem == null)
                throw new ItemNotFoundException("Mehsul Tapilmadi");

            _wishItemWriteRepository.Remove(wishItem);
            await _wishItemWriteRepository.SaveAsync();
            return wishItem;
        }
    }
}
