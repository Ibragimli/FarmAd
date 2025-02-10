using FarmAd.Domain.Entities;
using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Infrastructure.Service;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.SubCategory;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminCategoryServices : IAdminCategoryServices
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IImageManagerService _manageImageHelper;

        public AdminCategoryServices(ICategoryReadRepository categoryReadRepository, ISubCategoryReadRepository subCategoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IImageManagerService manageImageHelper)
        {
            _categoryReadRepository = categoryReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _manageImageHelper = manageImageHelper;
        }
        public async Task CategoryCreate(Category category)
        {
            Category newCategory = new Category();
            if (await _categoryReadRepository.IsExistAsync(x => x.Name == category.Name))
                throw new ItemAlreadyException("Bu adda kategoriya mövcuddur!");

            bool check = false;
            if (category.CategoryImageFile != null)
                _manageImageHelper.ValidateProduct(category.CategoryImageFile);

            if (category.Name != null)
            {
                if (category.Name.Length > 50)
                    throw new ItemFormatException("Kategoriyanın maksimum  uzunluğu 50 ola bilər");
                newCategory.Name = category.Name;
                check = true;
            }
            else
                throw new ItemNullException("Kategoriya boş ola bilməz");


            string imageFilename = ProductImageCreate(category);

            if (imageFilename != "false")
            {
                newCategory.Image = imageFilename;
                check = true;
            }

            if (check)
            {
                await _categoryWriteRepository.AddAsync(newCategory);
                await _categoryWriteRepository.SaveAsync();
            }
        }

        public async Task CategoryEdit(Category category)
        {
            bool check = false;
            var oldCategory = await _categoryReadRepository.GetAsync(x => x.Id == category.Id);
            if (await _categoryReadRepository.IsExistAsync(x => x.Name == category.Name && x.Id != category.Id))
                throw new ItemAlreadyException("Bu adda kategoriya mövcuddur!");

            if (category.CategoryImageFile != null)
                _manageImageHelper.ValidateProduct(category.CategoryImageFile);

            if (category.Name != null)
            {
                if (category.Name.Length > 50)
                    throw new ItemFormatException("Kategoriyanın maksimum  uzunluğu 50 ola bilər");
                if (category.Name != oldCategory.Name)
                {
                    oldCategory.Name = category.Name;
                    check = true;
                }
            }
            else
                throw new ItemNullException("Kategoriya boş ola bilməz");


            string imageFilename = ProductImageChange(category, oldCategory);

            if (imageFilename != "false")
            {
                oldCategory.Image = imageFilename;
                check = true;
            }

            if (check)
            {
                oldCategory.ModifiedDate = DateTime.UtcNow.AddHours(4);
                await _categoryWriteRepository.SaveAsync();
            }
        }

        public async Task CategoryDelete(int id)
        {
            var oldCategory = await _categoryReadRepository.GetAsync(x => x.Id == id);
            bool check = await _subCategoryReadRepository.IsExistAsync(x => x.CategoryId == id);
            if (check)
                throw new ItemAlreadyException("Kategoriya altkateqoriyalarda istifadə olunur!!!");
            ProductImageDelete(oldCategory);
            _categoryWriteRepository.Remove(oldCategory);
            await _categoryWriteRepository.SaveAsync();
        }
        private void ProductImageDelete(Category oldCategory)
        {
            var Image = oldCategory.Image;
            if (Image == null) throw new ImageNullException("Şəkil tapılmadı!");
            _manageImageHelper.DeleteFile(Image, "category");
        }
        private string ProductImageCreate(Category category)
        {
            if (category.CategoryImageFile != null)
            {
                var ProductImageFile = category.CategoryImageFile;
                string filename = _manageImageHelper.FileSave(ProductImageFile, "category");
                return filename;
            }
            else
                throw new ImageNullException("Şəkil hissəsi boş ola bilməz!");
        }
        private string ProductImageChange(Category category, Category oldCategory)
        {
            if (category.CategoryImageFile != null)
            {
                var ProductImageFile = category.CategoryImageFile;

                var Image = oldCategory.Image;

                if (Image == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(ProductImageFile, "category");
                _manageImageHelper.DeleteFile(Image, "category");
                Image = filename;

                return filename;
            }
            return "false";
        }
        public async Task<Category> GetCategory(int id)
        {
            var category = await _categoryReadRepository.GetAsync(x => x.Id == id && !x.IsDelete);

            return category;
        }
        public IQueryable<Category> GetCategories(string name)
        {
            var category = _categoryReadRepository.AsQueryable();
            category = category.Where(x => !x.IsDelete);
            if (name != null)
                category = category.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return category;
        }
    }
}
