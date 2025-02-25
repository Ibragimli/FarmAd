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
using FarmAd.Application.Abstractions.Storage;
using AutoMapper;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.City;

namespace FarmAd.Persistence.Services.User
{

    public class ProductCreateServices : IProductCreateServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICityReadRepository _cityReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        private readonly IProductUserIdWriteRepository _productUserIdWriteRepository;
        private readonly IUserService _userService;
        private readonly IUserAuthenticationWriteRepository _userAuthenticationWriteRepository;
        private readonly IUserAuthenticationReadRepository _userAuthenticationReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductFeatureWriteRepository _productFeatureWriteRepository;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IImageManagerService _manageImageHelper;
        private readonly IEmailServices _emailServices;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductCreateServices(UserManager<AppUser> userManager, ICityReadRepository cityReadRepository, ISubCategoryReadRepository subCategoryReadRepository, IMapper mapper, IStorageService storageService, IProductUserIdWriteRepository productUserIdWriteRepository, IUserService userService, IUserAuthenticationWriteRepository userAuthenticationWriteRepository, IUserAuthenticationReadRepository userAuthenticationReadRepository, IProductWriteRepository productWriteRepository, IProductFeatureWriteRepository productFeatureWriteRepository, IProductImageWriteRepository productImageWriteRepository, IImageManagerService manageImageHelper, IEmailServices emailServices, IHttpContextAccessor contextAccessor) : base()
        {
            _userManager = userManager;
            _cityReadRepository = cityReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _mapper = mapper;
            _storageService = storageService;
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
        public async Task CreateImagesAsync(List<IFormFile> imageFiles, int productId)
        {
            List<ProductImage> productImages = new();

            for (int i = 0; i < imageFiles.Count; i++)
            {
                var (fileName, path) = _storageService.UploadAsync("files\\products", imageFiles[i]);

                productImages.Add(new ProductImage
                {
                    IsProduct = i == 0, // İlk resim için true, diğerleri false olacak
                    ProductId = productId,
                    Image = fileName,
                    Path = path
                });
            }
            await _productImageWriteRepository.AddRangeAsync(productImages);
            await _productImageWriteRepository.SaveAsync();
        }
        public void CreateProductCookie(List<IFormFile> imageFiles, ProductCreateDto ProductCreateDto)
        {
            foreach (var item in imageFiles)
            {
                var (filename, path) = _storageService.UploadAsync("files\\products", item); ;

                ProductCreateDto.ImageFilesStr.Add(filename);
            }
            var ProductImageStr = JsonConvert.SerializeObject(ProductCreateDto.ImageFilesStr);
            _contextAccessor.HttpContext.Response.Cookies.Append("ProductImageFiles", ProductImageStr);
            ProductCreateDto.ImageFiles = null;
            var ProductStr = JsonConvert.SerializeObject(ProductCreateDto);
            _contextAccessor.HttpContext.Response.Cookies.Append("ProductVM", ProductStr);
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

        //public async Task CreateImageString(List<string> imageFiles, int ProductId)
        //{
        //    int i = 1;
        //    bool ProductStatus;
        //    foreach (var image in imageFiles)
        //    {
        //        ProductStatus = false;
        //        if (i == 1)
        //            ProductStatus = true;
        //        ProductImage Productimage = new ProductImage
        //        {
        //            IsProduct = ProductStatus,
        //            ProductId = ProductId,
        //            Image = image,
        //        };
        //        await _productImageWriteRepository.AddAsync(Productimage);

        //        i++;
        //    }
        //    await _productImageWriteRepository.SaveAsync();
        //}

        public async Task<ProductFeature> CreateProductFeature(ProductCreateDto ProductDto)
        {
            await ValidateProductCreateDto(ProductDto); // ✅ Doğrulamaları merkezi olarak yap

            var productFeature = _mapper.Map<ProductFeature>(ProductDto);
            productFeature.ProductStatus = ProductStatus.Waiting;
            //ProductFeature features = new ProductFeature
            //{
            //    Name = ProductDto.ProductName,
            //    CityId = ProductDto.CityId,
            //    Describe = ProductDto.Describe,
            //    Email = ProductDto.Email,
            //    PhoneNumber = ProductDto.PhoneNumber,
            //    SubCategoryId = ProductDto.SubCategoryId,
            //    Price = ProductDto.Price,
            //    PriceCurrency = ProductDto.PriceCurrency,
            //    IsShipping = ProductDto.IsShipping,
            //    IsNew = ProductDto.IsNew,
            //    ViewCount = 0,
            //    WishCount = 0,
            //    IsPremium = false,
            //    IsVip = false,
            //    ProductStatus = ProductStatus.Waiting,
            //    IsDisabled = false,
            //    ModifiedDate = DateTime.UtcNow.AddHours(4),
            //    IsDelete = false,

            //};



            try
            {
                await _productFeatureWriteRepository.AddAsync(productFeature);
                await _productFeatureWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Məlumat saxlanarkən xəta baş verdi: {ex.InnerException?.Message ?? ex.Message}");
            }

            //await _productFeatureWriteRepository.AddAsync(productFeature);
            //await _productFeatureWriteRepository.SaveAsync();
            return productFeature;
        }
        private async Task ValidateProductCreateDto(ProductCreateDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto), "Elan məlumatları göndərilməlidir!");

            if (productDto.SubCategoryId <= 0)
                throw new ItemNullException("Alt kategoriya hissəsi boş olmamalıdır!");

            bool subCategoryExists = await _subCategoryReadRepository.IsExistAsync(x => x.Id == productDto.SubCategoryId);
            if (!subCategoryExists)
                throw new ItemNotFoundException("Seçtiyiniz altkategoriya mövcud deyil!");

            if (!await _cityReadRepository.IsExistAsync(x => x.Id == productDto.CityId))
                throw new ItemNullException("Göstərilən CityId mövcud deyil!");
            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new ItemNullException("Elanın adı boş ola bilməz!");


            if (string.IsNullOrWhiteSpace(productDto.Name) || productDto.Name.Length < 3)
                throw new ItemFormatException("Elanın adı minimum 3 simvol olmalıdır!");

            if (productDto.Price <= 0)
                throw new ItemFormatException("Qiymət 0-dan çox olmalıdır!");

            if (productDto.CityId <= 0)
                throw new ItemNullException("Şəhər seçilməlidir!");

            if (string.IsNullOrWhiteSpace(productDto.Describe))
                throw new ItemNullException("Elan təsviri boş ola bilməz!");
        }

        public async Task<Product> CreateProduct(ProductFeature features)
        {
            Product Product = new Product
            {
                ProductFeatureId = features.Id,
            };
            try
            {
                await _productWriteRepository.AddAsync(Product);
                await _productWriteRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Məlumat saxlanarkən xəta baş verdi: {ex.InnerException?.Message ?? ex.Message}");
            }
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
                    _manageImageHelper.DeleteFile(image, "Product");
                }
                authentication.IsDisabled = true;
                _contextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                _contextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
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
                        _manageImageHelper.DeleteFile(image, "Product");
                    }
                    authentication.IsDisabled = true;
                    _contextAccessor.HttpContext.Response.Cookies.Delete("ProductVM");
                    _contextAccessor.HttpContext.Response.Cookies.Delete("ProductImageFiles");
                }
                await _userAuthenticationWriteRepository.SaveAsync();

                throw new AuthenticationCodeException("Kod yanlışdır!");
            }
            return authentication;
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
    
        public async Task CreateProductUserId(string userId, int ProductId)
        {
            ProductUserId ProductUserId = new ProductUserId();

            //Product user elaqesi
            ProductUserId = new ProductUserId
            {
                AppUserId = userId,
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
