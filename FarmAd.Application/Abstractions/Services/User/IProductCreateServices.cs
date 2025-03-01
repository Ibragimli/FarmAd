using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.DTOs;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductCreateServices
    {

        Task CreateImagesAsync(List<IFormFile> imageFiles, int productId);
        Task CreateImagesAsync(List<string> imageFiles, List<string> imagesPath, int productId,string username);

        Task<string> CreateOTPCode(string username);
        Task<ProductFeature> CreateProductFeature(ProductCreateDto ProductDto);
        //Task CreateImageString(List<string> imageFiles, int ProductId);
        //void CreateProductCookie(List<IFormFile> imageFiles, ProductCreateDto ProductCreateDto);
        Task<Product> CreateProduct(ProductFeature features);
        Task<Product> CreateProductForm(ProductFeature features, List<IFormFile> imageFiles);
        //ProductCreateDto GetProductCookie();
        List<string> GetImageFilesCookie();
        Task CreateProductUserId(string userId, int ProductId);
        Task ChangeAuthenticationStatus(UserAuthentication authentication);
        Task CreateProductRedisAsync(List<IFormFile> imageFiles, ProductCreateDto productCreateDto);
        Task<ProductCreateDto> GetProductFromRedisAsync(string username);

        void SendCode(string email, string code);

    }
}
