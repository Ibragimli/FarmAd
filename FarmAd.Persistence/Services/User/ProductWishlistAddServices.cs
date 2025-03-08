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

namespace FarmAd.Persistence.Services.User
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

        public async Task IsProduct(int id)
        {
            if (!await _productReadRepository.IsExistAsync(id))
            {
                throw new ItemNotFoundException("Elan tapılmadı!");
            }
            if (await _productReadRepository.IsExistAsync(x => x.ProductFeatures.ProductStatus != ProductStatus.Active && x.Id == id))
            {
                throw new ItemFormatException("Elan aktiv deyil!");
            }
        }
        public void CookieAddWish(int id)
        {
            List<CookieWishItemDto> wishItems = new();
            string existWishItem = _httpContextAccessor.HttpContext.Request.Cookies["wishItemList"];

            if (!string.IsNullOrEmpty(existWishItem))
            {
                wishItems = JsonConvert.DeserializeObject<List<CookieWishItemDto>>(existWishItem);
            }

            if (!wishItems.Any(x => x.ProductId == id)) // Mövcud olub-olmadığını yoxlayırıq
            {
                wishItems.Add(new CookieWishItemDto { ProductId = id });

                var serializedWishItems = JsonConvert.SerializeObject(wishItems);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("wishItemList", serializedWishItems);
            }

            var wishData = _getCookieWishItems(wishItems);
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

            return _getUserWishItems(await _wishItemReadRepository.GetAllAsync(x => x.AppUserId == user.Id));
        }
        private async Task<WishProductCreateDto> _getCookieWishItems(List<CookieWishItemDto> cookieWishItems)
        {
            WishProductCreateDto wishItems = new WishProductCreateDto()
            {
                WishItems = new List<WishItemsDto>(),
            };

            foreach (var item in cookieWishItems)
            {
                var product = await _productReadRepository.GetAsync(x => x.Id == item.ProductId, "ProductFeatures");

                if (product?.ProductFeatures != null) // Null yoxlanışı əlavə edirik
                {
                    WishItemsDto wishItem = new WishItemsDto
                    {
                        Name = product.ProductFeatures.Name,
                        Price = (decimal)product.ProductFeatures.Price,
                        ProductId = product.Id,
                    };
                    wishItems.WishItems.Add(wishItem); // 🔥 Burada `wishItems` listinə əlavə edirik.
                }
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
