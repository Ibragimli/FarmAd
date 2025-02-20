using FarmAd.Domain.Entities;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.Area;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.Category;
using FarmAd.Application.Repositories.ProductFeature;
using FarmAd.Application.DTOs.Area;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminSubCategoryServices : IAdminSubCategoryServices
    {
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductFeatureReadRepository _productFeatureReadRepository;
        private readonly ISubCategoryWriteRepository _subCategoryWriteRepository;

        public AdminSubCategoryServices(ISubCategoryReadRepository subCategoryReadRepository, ICategoryReadRepository categoryReadRepository, IProductFeatureReadRepository productFeatureReadRepository, ISubCategoryWriteRepository subCategoryWriteRepository)
        {
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _productFeatureReadRepository = productFeatureReadRepository;
            _subCategoryWriteRepository = subCategoryWriteRepository;
        }
        public async Task SubCategoryCreate(SubCategoryCreateDto subCategoryCreateDto)
        {
            if (string.IsNullOrWhiteSpace(subCategoryCreateDto.Name))
                throw new ItemNullException("Alt kategori adı boş ola bilməz!");

            if (await _subCategoryReadRepository.IsExistAsync(x => x.Name == subCategoryCreateDto.Name))
                throw new ItemAlreadyException("Bu adda altkategoriya mövcuddur!");
            if (subCategoryCreateDto.Name.Length > 100)
                throw new ItemFormatException("Alt kategori adı maksimum uzunluğu 100 ola bilər!");

            if (subCategoryCreateDto.CategoryId == 0 || !await _categoryReadRepository.IsExistAsync(x => x.Id == subCategoryCreateDto.CategoryId))

                throw new ItemNullException("Kategoriya adı boş ola bilməz!");

            var newSubCategory = new SubCategory
            {
                Name = subCategoryCreateDto.Name,
                CategoryId = subCategoryCreateDto.CategoryId
            };

            await _subCategoryWriteRepository.AddAsync(newSubCategory);
            await _subCategoryWriteRepository.SaveAsync();
        }

        public async Task SubCategoryUpdate(SubCategoryUpdateDto subCategory)
        {
            var oldSubCategory = await _subCategoryReadRepository.GetAsync(x => x.Id == subCategory.Id);
            if (oldSubCategory == null)
                throw new ItemNotFoundException("Alt kategori tapılmadı!");

            if (string.IsNullOrWhiteSpace(subCategory.Name))
                throw new ItemNullException("AltKategoriya adı boş ola bilməz!");

            if (subCategory.Name.Length > 100)
                throw new ItemFormatException("AltKategoriya adının maksimum uzunluğu 100 ola bilər!");

            if (subCategory.Name != oldSubCategory.Name)
            {
                if (await _subCategoryReadRepository.IsExistAsync(x => x.Name == subCategory.Name))
                    throw new ItemAlreadyException("Bu adda altkategoriya mövcuddur!");

                oldSubCategory.Name = subCategory.Name;
            }
            if (subCategory.CategoryId == 0 || !await _categoryReadRepository.IsExistAsync(x => x.Id == subCategory.CategoryId))
                throw new ItemAlreadyException("Kategoriya tapılmadı!");

            oldSubCategory.CategoryId = subCategory.CategoryId;
            oldSubCategory.ModifiedDate = DateTime.UtcNow.AddHours(4);

            await _subCategoryWriteRepository.SaveAsync();
        }

        public async Task SubCategoryDelete(int id)
        {
            var oldSubCategory = await _subCategoryReadRepository.GetAsync(x => x.Id == id);
            if (oldSubCategory == null)
                throw new ItemNotFoundException("Alt kategori tapılmadı!");

            if (await _productFeatureReadRepository.IsExistAsync(x => x.SubCategoryId == id))
                throw new ItemUseException("Altkategoriya elanda istifadə olunur!");

            _subCategoryWriteRepository.Remove(oldSubCategory);
            await _subCategoryWriteRepository.SaveAsync();
        }

        public async Task<SubCategory> GetSubCategory(int id)
        {
            var subCategory = await _subCategoryReadRepository.GetAsync(x => x.Id == id && !x.IsDelete, "Category");
            if (subCategory == null)
                throw new ItemNullException("Alt kategoriya tapılmadı");
            return subCategory;
        }
        public (object, int) GetSubCategorys(string subCategory, string category, int page, int size)
        {
            int subCount = _subCategoryReadRepository.GetAll().Count();
            var SubCategory = _subCategoryReadRepository.GetAllPagenated(page, size).Where(x => !x.IsDelete);
            if (category != null)
                SubCategory = SubCategory.Where(i => EF.Functions.Like(i.Category.Name, $"%{category}%"));
            if (subCategory != null)
                SubCategory = SubCategory.Where(i => EF.Functions.Like(i.Name, $"%{subCategory}%"));

            return (SubCategory, subCount);
        }
        public async Task<List<Category>> GetCategories()
        {
            var category = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete);
            return category.ToList();
        }


    }
}
