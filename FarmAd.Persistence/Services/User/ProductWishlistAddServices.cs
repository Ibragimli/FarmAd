using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

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

namespace Ferma.Service.Services.Implementations.User
{
    public class ProductWishlistAddServices : IProductWishlistAddServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWishItemReadRepository _wishItemReadRepository;
        private readonly IWishItemWriteRepository _wishItemWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProductWishlistAddServices(IHttpContextAccessor httpContextAccessor, IWishItemReadRepository wishItemReadRepository, IWishItemWriteRepository wishItemWriteRepository, IProductReadRepository productReadRepository, UserManager<AppUser> userManager) : base()
        {
            _httpContextAccessor = httpContextAccessor;
            _wishItemReadRepository = wishItemReadRepository;
            _wishItemWriteRepository = wishItemWriteRepository;
            _productReadRepository = productReadRepository;
            _userManager = userManager;
        }
        public void CookieAddWish(int id)
        {
            List<CookieWishItemDto> wishItems = new List<CookieWishItemDto>();
            string existWishItem = _httpContextAccessor.HttpContext.Request.Cookies["wishItemList"];
            if (existWishItem != null)
            {
                wishItems = JsonConvert.DeserializeObject<List<CookieWishItemDto>>(existWishItem);
            }
            CookieWishItemDto item = wishItems.Find(x => x.ProductId == id);

            if (item == null)
            {
                item = new CookieWishItemDto
                {
                    ProductId = id,
                };
                wishItems.Add(item);
            }

            var ProductIdStr = JsonConvert.SerializeObject(wishItems);
            _httpContextAccessor.HttpContext.Response.Cookies.Append("wishItemList", ProductIdStr);
            var wishData = _getCookieWishItems(wishItems);
        }
        public async Task<AppUser> IsAuthenticated()
        {
            AppUser user = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            return user;
        }
        public async Task IsProduct(int id)
        {
            if (!await _productReadRepository.IsExistAsync(id))
            {
                throw new ItemNotFoundException("Mehsul Tapilmadi");
            }
            if (await _productReadRepository.IsExistAsync(x => x.ProductFeatures.ProductStatus != ProductStatus.Active && x.Id == id))
            {
                throw new ItemFormatException("Elan aktiv deyil!");
            }
        }
        public async Task<WishProductCreateDto> UserAddWish(int id, AppUser user)
        {
            WishItem wishItem = await _wishItemReadRepository.GetAsync(x => x.AppUserId == user.Id && x.ProductId == id, "Product", "Product.ProductFeatures");

            if (wishItem == null)
            {
                wishItem = new WishItem
                {
                    AppUserId = user.Id,
                    ProductId = id,
                };
                await _wishItemWriteRepository.AddAsync(wishItem);
                await _wishItemWriteRepository.SaveAsync();
            }


            var wishData = _getUserWishItems(await _wishItemReadRepository.GetAllAsync(x => x.AppUserId == user.Id));
            return wishData;
        }
        private async Task<WishProductCreateDto> _getCookieWishItems(List<CookieWishItemDto> cookieWishItems)
        {
            WishProductCreateDto wishItems = new WishProductCreateDto()
            {
                WishItems = new List<WishItemsDto>(),
            };

            foreach (var item in cookieWishItems)
            {

                var Product = await _productReadRepository.GetAsync(x => x.Id == item.ProductId, "ProductFeatures");
                WishItemsDto wishItem = new WishItemsDto
                {
                    Name = Product.ProductFeatures.Name,
                    Price = (decimal)Product.ProductFeatures.Price,
                    ProductId = Product.Id,
                };
            }
            return wishItems;
        }
        private WishProductCreateDto _getUserWishItems(IEnumerable<WishItem> wishItems)
        {
            WishProductCreateDto wish = new WishProductCreateDto
            {
                WishItems = new List<WishItemsDto>(),
            };
            foreach (var item in wishItems)
            {
                WishItemsDto wishItem = new WishItemsDto
                {
                    Name = item.Product.ProductFeatures.Name,
                    Price = (decimal)item.Product.ProductFeatures.Price,
                    ProductId = item.Product.Id,
                };
                wish.WishItems.Add(wishItem);
            }
            return wish;
        }

    }
}
