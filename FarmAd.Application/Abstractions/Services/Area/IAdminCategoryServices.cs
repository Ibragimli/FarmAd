using FarmAd.Application.DTOs.Area;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminCategoryServices
    {
        public (object, int) GetCategories(string name, int page, int size);
        public Task<Category> GetCategory(int id);
        public Task CategoryCreate(CategoryCreateDto categoryCreateDto);
        public Task CategoryEdit(CategoryUpdateDto categoryUpdateDto);
        public Task CategoryDelete(int id);
    }
}
