using FarmAd.Application.DTOs.Area;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminSubCategoryServices
    {
        public (object,int) GetSubCategorys(string subCategory, string category, int page,int size);
        public Task<SubCategory> GetSubCategory(int id);
        public Task<List<Category>> GetCategories();
        public Task SubCategoryCreate(SubCategoryCreateDto subCategoryCreateDto);
        public Task SubCategoryUpdate(SubCategoryUpdateDto subCategory);
        public Task SubCategoryDelete(int id);
    }
}
