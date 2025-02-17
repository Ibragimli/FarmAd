using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ImageSetting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Infrastructure.Service
{
    public class ImageManagerService : IImageManagerService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IImageSettingReadRepository _imageSettingReadRepository;

        public ImageManagerService(IWebHostEnvironment env, IImageSettingReadRepository imageSettingReadRepository)
        {
            _env = env;
            _imageSettingReadRepository = imageSettingReadRepository;
        }

        public string GetValue(string key)
        {
            var setting = _imageSettingReadRepository
                .GetWhere(x => !x.IsDelete)
                .FirstOrDefault(x => x.Key == key)?.Value;

            if (string.IsNullOrEmpty(setting))
                throw new Exception($"Şəkil dəyəri boş ola bilməz: {key}");

            return setting;
        }

        public int GetValueInt(string key)
        {
            return int.TryParse(GetValue(key), out int value)
                ? value
                : throw new Exception($"Yanlış format: {key}");
        }

        public void ValidateProduct(IFormFile ProductImageFile)
        {
            string[] allowedTypes = { GetValue("ImageType1"), GetValue("ImageType2") };
            int maxSize = GetValueInt("ImageSize") * 1048576; // MB -> Byte dönüşümü

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

        //public string FileSave(IFormFile image, string folderName)
        //{
        //    return FileManager.Save(_env.WebRootPath, $"uploads/{folderName}", image);
        //}

        //public void FileDelete(string image, string folderName)
        //{
        //    FileManager.Delete(_env.WebRootPath, $"uploads/{folderName}", image);
        //}


        public void DeleteFile(string image, string folderName)
        {
            throw new NotImplementedException();
        }

        public string FileSave(IFormFile Image, string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
