using FarmAd.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductCreateValueCheckServices
    {
        void CheckDescribe(string describe);

        Task SubCategoryValidation(int subCategoryId);
        Task CityValidation(int cityId);
        void ImageCheck(List<IFormFile> images);
    }
}
