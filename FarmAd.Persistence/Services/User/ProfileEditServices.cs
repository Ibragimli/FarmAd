
using FarmAd.Application.Exceptions;
using FarmAd.Application.DTOs.User;
using FarmAd.Application.Abstractions.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Infrastructure.Service.User;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.SubCategory;
using FarmAd.Application.Repositories.Category;

namespace FarmAd.Persistence.Service.User
{
    public class ProfileEditServices : IProfileEditServices
    {
        private readonly IUserService _userService;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ISubCategoryReadRepository _subCategoryReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProfileEditServices(IUserService userService, IProductReadRepository productReadRepository, ISubCategoryReadRepository subCategoryReadRepository, ICategoryReadRepository categoryReadRepository, IProductWriteRepository productWriteRepository)
        {
            _userService = userService;
            _productReadRepository = productReadRepository;
            _subCategoryReadRepository = subCategoryReadRepository;
            _categoryReadRepository = categoryReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task CheckValue(ProfileEditDto editDto)
        {
            var user = await _userService.GetAsync(x => x.Id == editDto.UserId);

            if (editDto.UserId == null)
                throw new NotFoundException("user404");

            if (editDto.Email == null && editDto.Name == null)
                throw new ItemNullException("Email Null");

        }

        public async Task Edit(ProfileEditDto editDto)
        {
            var user = await _userService.GetAsync(x => x.Id == editDto.UserId);
            bool checkBool = false;
            if (user.Email != editDto.Email)
                if (editDto.Email != null)
                {
                    user.Email = editDto.Email;
                    user.NormalizedEmail = editDto.Email.ToUpper();
                    checkBool = true;
                }

            if (user.Name != editDto.Name)
                if (editDto.Name != null)
                {
                    user.Name = editDto.Name;
                    checkBool = true;
                }
            if (checkBool)
                await _productWriteRepository.SaveAsync();
        }

        public async Task<ProductEditGetDto> EditVM(int id)
        {
            ProductEditGetDto ProductEditVM = new ProductEditGetDto
            {
                ProductEditDto = new ProductEditDto(),

                Product = await _productReadRepository.GetAsync(x => x.Id == id && x.IsDelete == false && x.ProductFeatures.IsDisabled == false,
                "ProductImages", "ProductFeatures.SubCategory", "ProductFeatures.SubCategory.Category", "ProductUserIds.AppUser", "ProductFeatures.City"),

                Categories = await _categoryReadRepository.GetAllAsync(x => !x.IsDelete),
                SubCategories = await _subCategoryReadRepository.GetAllAsync(x => !x.IsDelete),
            };
            return ProductEditVM;
        }
    }
}
