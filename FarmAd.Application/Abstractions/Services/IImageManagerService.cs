using FarmAd.Application.Abstractions.Helpers;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Repositories.ImageSetting;
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
        public string GetValue(string key);
        public int GetValueInt(string key);
        public void ValidateProduct(IFormFile ProductImageFile);
        public void ValidateImages(List<IFormFile> Images);
        public string FileSave(IFormFile Image, string folderName);
        public void DeleteFile(string image, string folderName);
    }
}
