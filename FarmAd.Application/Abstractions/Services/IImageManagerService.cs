using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ImageSetting;
using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services
{
    public interface IImageManagerService
    {
        Task<string> GetValue(string key);
        Task<int> GetValueInt(string key);
        Task ValidateProduct(IFormFile ProductImageFile);
        public void ValidateImages(List<IFormFile> Images);
        ProductImage AddImage(int productId, IFormFile image);
        List<ProductImage> AddImages(int productId, List<IFormFile> images, bool isPoster = false);
        List<ProductImage> AddImages(int productId, List<string> imageFiles, List<string> imagesPath, bool isPoster = false);


    }
}
