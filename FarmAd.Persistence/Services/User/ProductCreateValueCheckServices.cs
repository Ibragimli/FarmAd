
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FarmAd.Application.Repositories.SubCategory;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using FarmAd.Application.Repositories.City;

namespace FarmAd.Persistence.Services.User
{
    public class ProductCreateValueCheckServices : IProductCreateValueCheckServices
    {
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICityReadRepository _cityReadRepository;

        public ProductCreateValueCheckServices(ISubCategoryReadRepository subCategoryReadRepository, ICityReadRepository cityReadRepository)
        {
            _subCategoryReadRepository = subCategoryReadRepository;
            _cityReadRepository = cityReadRepository;
        }

        public void CheckDescribe(string describe)
        {
            if (describe == null)
            {
                throw new ItemNullException("Təsvir hissəsi boş ola bilməz!");
            }
        }
        public async Task SubCategoryValidation(int subCategoryId)
        {
            if (subCategoryId == 0)
                throw new ItemNullException("Kategoriyanı  qeyd edin");

            bool check = await _subCategoryReadRepository.IsExistAsync(subCategoryId);
            if (!check)
                throw new ItemNotFoundException("Düzgün kategoriya seçilməyib");
        }


        public void ImageCheck(List<IFormFile> images)
        {
            if (images == null)
                throw new ImageNullException("Şəkil yüklənilməlidir");

        }

        public async Task CityValidation(int cityId)
        {
            bool check = await _cityReadRepository.IsExistAsync(x => x.Id == cityId);
            if (!check)
                throw new ItemNotFoundException("Düzgün şəhər seçilməyib");
        }
    }
}
