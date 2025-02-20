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
using FarmAd.Application.DTOs.Area;
using FarmAd.Application.Abstractions.Storage.Local;
using FarmAd.Application.Abstractions.Storage;

namespace FarmAd.Persistence.Service.Area
{
    public class AdminCategoryServices : IAdminCategoryServices
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IStorageService _storageService;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IImageManagerService _manageImageHelper;

        public AdminCategoryServices(ICategoryReadRepository categoryReadRepository, IStorageService storageService, ISubCategoryReadRepository subCategoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IImageManagerService manageImageHelper)
        {
            _categoryReadRepository = categoryReadRepository;
            _storageService = storageService;
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _manageImageHelper = manageImageHelper;
        }
        public async Task CategoryCreate(CategoryCreateDto categoryCreateDto)
        {
            if (await _categoryReadRepository.IsExistAsync(x => x.Name == categoryCreateDto.Name))
                throw new ItemAlreadyException("Bu adda kategoriya mövcuddur!");

            if (categoryCreateDto.Image != null)
                _manageImageHelper.ValidateProduct(categoryCreateDto.Image);

            if (categoryCreateDto.Name == null)
                throw new ItemNullException("Kategoriya boş ola bilməz");

            if (categoryCreateDto.Name.Length > 50)
                throw new ItemFormatException("Kategoriyanın maksimum  uzunluğu 50 ola bilər");

            Category newCategory = new Category();

            var image = _storageService.UploadAsync("files\\categories", categoryCreateDto.Image);

            newCategory.Name = categoryCreateDto.Name;
            newCategory.Image = image.fileName;
            newCategory.ImagePath = image.path;


            await _categoryWriteRepository.AddAsync(newCategory);
            await _categoryWriteRepository.SaveAsync();
        }

        public async Task CategoryEdit(CategoryUpdateDto categoryUpdateDto)
        {
            if (await _categoryReadRepository.IsExistAsync(x => x.Name == categoryUpdateDto.Name && x.Id != categoryUpdateDto.Id))
                throw new ItemAlreadyException("Bu adda kategoriya mövcuddur!");
            var oldCategory = await _categoryReadRepository.GetAsync(x => x.Id == categoryUpdateDto.Id);
            if (oldCategory == null)
                throw new ItemNullException("Kategoriya tapılmadı");
            if (categoryUpdateDto.Name.Length > 50)
                throw new ItemFormatException("Kategoriyanın maksimum  uzunluğu 50 ola bilər");

            if (categoryUpdateDto.Image != null)
                _manageImageHelper.ValidateProduct(categoryUpdateDto.Image);

            var newImage = _storageService.UploadAsync("files\\categories", categoryUpdateDto.Image);

            oldCategory.Name = categoryUpdateDto.Name;
            oldCategory.Image = newImage.fileName;
            oldCategory.ImagePath = newImage.path;


            oldCategory.ModifiedDate = DateTime.UtcNow.AddHours(4);
            await _categoryWriteRepository.SaveAsync();
        }

        public async Task CategoryDelete(int id)
        {
            var oldCategory = await _categoryReadRepository.GetAsync(x => x.Id == id);
            if (oldCategory == null)
                throw new ItemNullException("Kategoriya tapılmadı");
            if (await _subCategoryReadRepository.IsExistAsync(x => x.CategoryId == id))
                throw new ItemAlreadyException("Kategoriya altkateqoriyalarda istifadə olunur!!!");

            _storageService.DeleteAsync("files\\categories", oldCategory.Image);
            _categoryWriteRepository.Remove(oldCategory);
            await _categoryWriteRepository.SaveAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await _categoryReadRepository.GetAsync(x => x.Id == id && !x.IsDelete);

            return category;
        }
        public (object, int) GetCategories(string name, int page, int size)
        {
            int count = _categoryReadRepository.GetAll().Count();
            var category = _categoryReadRepository.GetAllPagenated(page, size).Where(x => !x.IsDelete);
            if (name != null)
                category = category.Where(i => EF.Functions.Like(i.Name, $"%{name}%"));

            return (category, count);
        }

    }
}
