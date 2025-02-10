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
        public IQueryable<SubCategory> GetSubCategorys(string category, string subCategory);
        public Task<SubCategory> GetSubCategory(int id);
        public Task<List<Category>> GetCategories();
        public Task SubCategoryCreate(SubCategory SubCategory);
        public Task SubCategoryEdit(SubCategory SubCategory);
        public Task SubCategoryDelete(int id);
    }
}
