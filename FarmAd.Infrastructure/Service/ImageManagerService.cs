using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Abstractions.Storage;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ImageSetting;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FarmAd.Infrastructure.Service
{
    public class ImageManagerService : IImageManagerService
    {
        private readonly IStorageService _storageService;
        private readonly IImageSettingReadRepository _imageSettingReadRepository;

        public ImageManagerService(IStorageService storageService, IImageSettingReadRepository imageSettingReadRepository)
        {
            _storageService = storageService;
        }

        public async Task<string> GetValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Açar boş ola bilməz", nameof(key));

            var setting = await _imageSettingReadRepository.Table.Where(x => x.Key == key).FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(setting.Value))
                throw new Exception($"Şəkil dəyəri boş ola bilməz: {key}");

            return setting.Value;
        }

        public async Task<int> GetValueInt(string key)
        {
            return int.TryParse(await GetValue(key), out int value)
                ? value
                : throw new Exception($"Yanlış format: {key}");
        }

        public async Task ValidateProduct(IFormFile ProductImageFile)
        {
            //string imageType1 = await GetValue("ImageType1");
            //string imageType2 = await GetValue("ImageType2");

            string[] allowedTypes = { "image/jpeg", "image/png" /*imageType1, *//*imageType2*/ };
            int maxSize = /*await GetValueInt("ImageSize")*/ 10 * 1048576; // MB -> Byte dönüşümü

            if (!allowedTypes.Contains(ProductImageFile.ContentType))
                throw new ImageFormatException("Şəkil yalnız (png və ya jpg) formatında ola bilər");

            if (ProductImageFile.Length > maxSize)
                throw new ImageFormatException($"Şəkilin maksimum yaddaşı {GetValueInt("ImageSize")}MB ola bilər!");
        }

        public void ValidateImages(List<IFormFile> images)
        {
            if (images.Count > 8)
                throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz");

            foreach (var image in images)
            {
                ValidateProduct(image);
            }
        }
        public ProductImage AddImage(int productId, IFormFile image)
        {
            var (fileName, path) = _storageService.Upload("files\\products", image);
            var prdImage = new ProductImage()
            {
                IsProduct = true,
                ProductId = productId,
                Image = fileName,
                Path = path
            };
            return prdImage;
        }
        public List<ProductImage> AddImages(int productId, List<IFormFile> images, bool isPoster = false)
        {
            List<ProductImage> productImages = new();

            for (int i = 0; i < images.Count; i++)
            {
                var (fileName, path) = _storageService.Upload($"files\\products", images[i]);

                productImages.Add(new ProductImage
                {
                    IsProduct = isPoster == false ? (i == 0) : false,
                    ProductId = productId,
                    Image = fileName,
                    Path = path
                });
            }
            return productImages;
        }
        public List<ProductImage> AddImages(int productId, List<string> imageFiles, List<string> imagesPath, bool isPoster = false)
        {
            List<ProductImage> productImages = new();

            for (int i = 0; i < imageFiles.Count; i++)
            {
                var imageFile = imageFiles[i];
                var imagePath = imagesPath[i];

                productImages.Add(new ProductImage
                {
                    IsProduct = i == 0,
                    Path = imagePath,
                    Image = imageFile,
                    ProductId = productId
                });
            }
            return productImages;
        }
    }
}
