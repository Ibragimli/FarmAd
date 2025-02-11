using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Entities.Identity;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Persistence.Repositories.ProductFeature;
using FarmAd.Persistence.Repositories.Product;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.UserAuthentication;
using FarmAd.Infrastructure.Service.User;

namespace FarmAd.Persistence.Services.User
{

    public class ProductCreateServices : IProductCreateServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ProductUserIdWriteRepository _productUserIdWriteRepository;
        private readonly IUserService _userService;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductFeatureWriteRepository _productFeatureWriteRepository;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IImageManagerService _manageImageHelper;
        private readonly IEmailServices _emailServices;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductCreateServices(UserManager<AppUser> userManager, ProductUserIdWriteRepository productUserIdWriteRepository, IUserService userService, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository, IProductWriteRepository productWriteRepository, IProductFeatureWriteRepository productFeatureWriteRepository, IProductImageWriteRepository productImageWriteRepository, IImageManagerService manageImageHelper, IEmailServices emailServices, IHttpContextAccessor contextAccessor) : base()
        {
            _userManager = userManager;
            _productUserIdWriteRepository = productUserIdWriteRepository;
            _userService = userService;
            _userAuthenticationWriteRepository = userAuthenticationWriteRepository;
            _userAuthenticationReadRepository = userAuthenticationReadRepository;
            _productWriteRepository = productWriteRepository;
            _productFeatureWriteRepository = productFeatureWriteRepository;
            _productImageWriteRepository = productImageWriteRepository;
            _manageImageHelper = manageImageHelper;
            _emailServices = emailServices;
            _contextAccessor = contextAccessor;
        }
        public async Task CreateImageFormFile(List<IFormFile> imageFiles, int ProductId)
        {
            int i = 1;
            bool ProductStatus;
            foreach (var image in imageFiles)
            {
                ProductStatus = false;
                if (i == 1)
                    ProductStatus = true;
                ProductImage Productimage = new ProductImage
                {
                    IsProduct = ProductStatus,
                    ProductId = ProductId,
                    Image = _manageImageHelper.FileSave(image, "Product"),
                };
                await _productImageWriteRepository.AddAsync(Productimage);
                i++;
            }
            await _productImageWriteRepository.SaveAsync();
        }

        public async Task CreateImageString(List<string> imageFiles, int ProductId)
        {
            int i = 1;
            bool ProductStatus;
            foreach (var image in imageFiles)
            {
                ProductStatus = false;
                if (i == 1)
                    ProductStatus = true;
                ProductImage Productimage = new ProductImage
                {
                    IsProduct = ProductStatus,
                    ProductId = ProductId,
                    Image = image,
                };
                await _productImageWriteRepository.AddAsync(Productimage);

                i++;
            }
            await _productImageWriteRepository.SaveAsync();
        }

        public async Task<ProductFeature> CreateProductFeature(ProductCreateDto ProductDto)
        {
            ProductFeature features = new ProductFeature
            {
                Name = ProductDto.ProductName,
                CityId = ProductDto.CityId,
                Describe = ProductDto.Describe,
                Email = ProductDto.Email,
                PhoneNumber = ProductDto.PhoneNumber,
                SubCategoryId = ProductDto.SubCategoryId,
                Price = ProductDto.Price,
                PriceCurrency = ProductDto.PriceCurrency,
                IsShipping = ProductDto.IsShipping,
                IsNew = ProductDto.IsNew,
                ViewCount = 0,
                WishCount = 0,
                IsPremium = false,
                IsVip = false,
                ProductStatus = ProductStatus.Waiting,
                IsDisabled = false,
                ModifiedDate = DateTime.UtcNow.AddHours(4),
                IsDelete = false,

            };
            await _productFeatureWriteRepository.AddAsync(features);
            await _productFeatureWriteRepository.SaveAsync();
            return features;
        }

        public async Task<Product> CreateProduct(ProductFeature features)
        {
            Product Product = new Product
            {
                ProductFeatureId = features.Id,
            };
            await _productWriteRepository.AddAsync(Product);
            await _productWriteRepository.SaveAsync();
            return Product;
        }

        public async Task<Product> CreateProductForm(ProductFeature features, List<IFormFile> imageFiles)
        {
            Product Product = new Product
            {
                ProductFeatureId = features.Id,
                ImageFiles = imageFiles,
            };
            await _productWriteRepository.AddAsync(Product);
            await _productWriteRepository.SaveAsync();
            return Product;

        }

        //public async void SaveChange(Product Product)
        //{
        //    await _productReadRepository.InsertAsync(Product);
        //}
        //public async void SaveContext(Product Product)
        //{
        //    await _unitOfWork.CommitAsync();
        //}

        public void SendCode(string email, string code)
        {
            _emailServices.Send(email, "Doğrulama kodunuz", code);
        }
        public void CreateProductCookie(List<IFormFile> imageFiles, ProductCreateDto ProductCreateDto)
        {
            foreach (var item in imageFiles)
            {
                var filename = _manageImageHelper.FileSave(item, "Product");
                ProductCreateDto.ImageFilesStr.Add(filename);
            }
            var ProductImageStr = JsonConvert.SerializeObject(ProductCreateDto.ImageFilesStr);
            _contextAccessor.HttpContext.Response.Cookies.Append("ProductImageFiles", ProductImageStr);
            ProductCreateDto.ImageFiles = null;
            var ProductStr = JsonConvert.SerializeObject(ProductCreateDto);
            _contextAccessor.HttpContext.Response.Cookies.Append("ProductVM", ProductStr);
        }
        public async Task<UserAuthentication> CheckAuthentication(string code, string phoneNumber, string token, List<string> images)
        {
            var now = DateTime.UtcNow.AddHours(4).TimeOfDay;

            var authentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false && x.Code == code && x.Username == phoneNumber);
            var existAuthentication = await _userAuthenticationReadRepository.GetAsync(x => x.IsDisabled == false);
            if (existAuthentication == null)
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");

            if (existAuthentication.ExpirationDate.TimeOfDay < now)
            {
                foreach (var image in images)
                {
                    _manageImageHelper.DeleteFile(image, "Product");
                }
                existAuthentication.IsDisabled = true;
                _contextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                _contextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
                await _userAuthenticationWriteRepository.SaveAsync();
                throw new ExpirationDateException("Kodun müddəti bitmişdir! Təkrar giriş edin");

            }

            //Kod dogrulugunun yoxlanilmasi, təkrar yoxlama limiti
            if (authentication == null)
            {
                if (existAuthentication != null)
                {
                    if (existAuthentication.Count > 1)
                        existAuthentication.Count -= 1;
                    else
                    {
                        foreach (var image in images)
                        {
                            _manageImageHelper.DeleteFile(image, "Product");
                        }
                        existAuthentication.IsDisabled = true;
                        _contextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                        _contextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
                    }
                    await _userAuthenticationWriteRepository.SaveAsync();

                }
                throw new AuthenticationCodeException("Kod yanlışdır!");
            }
            return authentication;
        }
        public ProductCreateDto GetProductCookie()
        {
            ProductCreateDto ProductCreateDto = new ProductCreateDto();

            //cookie
            string ProductItem = _contextAccessor.HttpContext.Request.Cookies["ProductVM"];

            if (ProductItem != null)
                ProductCreateDto = JsonConvert.DeserializeObject<ProductCreateDto>(ProductItem);
            else
                throw new CookieNotActiveException("Cookie-nizi aktiv edin!");
            return ProductCreateDto;

        }
        public List<string> GetImageFilesCookie()
        {
            List<string> images = new List<string>();
            string imageItem = _contextAccessor.HttpContext.Request.Cookies["ProductImageFiles"];

            if (imageItem != null)
                images = JsonConvert.DeserializeObject<List<string>>(imageItem);
            else
                throw new CookieNotActiveException("Cookie-nizi aktiv edin!");
            return images;
        }
        public async Task<AppUser> CreateNewUser(string code, string phoneNumber, string email, string fullname)
        {
            AppUser newUser = new AppUser();
            //hesab yaradmaq

            var UserExists = await _userService.GetAsync(x => x.PhoneNumber == phoneNumber);
            if (UserExists == null)
            {
                newUser = new AppUser
                {
                    UserName = phoneNumber,
                    PhoneNumber = phoneNumber,
                    IsAdmin = false,
                    Balance = 0,
                    Email = email,
                    Fullname = fullname,
                };
                var result = await _userManager.CreateAsync(newUser, code);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await _userManager.AddToRoleAsync(newUser, "User");
                await _userAuthenticationWriteRepository.SaveAsync();
                return newUser;

            }
            return UserExists;
            //hesab yaradmaq
        }
        public async Task CreateProductUserId(string userId, int ProductId, AppUser user)
        {
            ProductUserId ProductUserId = new ProductUserId();

            //Product user elaqesi
            ProductUserId = new ProductUserId
            {
                AppUserId = user.Id,
                ProductId = ProductId,
            };
            await _productUserIdWriteRepository.AddAsync(ProductUserId);
            await _productUserIdWriteRepository.SaveAsync();
        }
        public async Task ChangeAuthenticationStatus(UserAuthentication authentication)
        {
            authentication.IsDisabled = true;
            _contextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
            _contextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
            await _userAuthenticationWriteRepository.SaveAsync();
        }
    }
}
