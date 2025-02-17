using FarmAd.Application.DTOs.User;
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
    
        Task<ProductFeature> CreateProductFeature(ProductCreateDto ProductDto);
        //Task CreateImageString(List<string> imageFiles, int ProductId);
        Task CreateImageFormFileAsync(List<IFormFile> imageFiles, int ProductId);
        void CreateProductCookie(List<IFormFile> imageFiles, ProductCreateDto ProductCreateDto);
        void SendCode(string email, string code);
        Task<Product> CreateProduct(ProductFeature features);
        Task<Product> CreateProductForm(ProductFeature features, List<IFormFile> imageFiles);
        //void SaveChange(Product Product);
        //void SaveContext(Product Product);

        Task<UserAuthentication> CheckAuthentication(string code, string phoneNumber,List<string> images);
        ProductCreateDto GetProductCookie();
        List<string> GetImageFilesCookie();
        Task<AppUser> CreateNewUser(string code, string phoneNumber, string email, string fullname);
        Task CreateProductUserId(string userId, int ProductId, AppUser user);
        Task ChangeAuthenticationStatus(UserAuthentication authentication);

    }
}
