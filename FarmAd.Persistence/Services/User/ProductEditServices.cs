using FarmAd.Domain.Entities;
using FarmAd.Domain.Enums;

using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Application.Abstractions.Services;
using FarmAd.Persistence.Repositories.Product;
using FarmAd.Application.Repositories.Product;
using FarmAd.Application.Repositories.ProductImage;
using FarmAd.Application.Abstractions.Storage;
using static System.Net.Mime.MediaTypeNames;
using FarmAd.Infrastructure.Service;
using FarmAd.Application.DTOs.User;
using Newtonsoft.Json;

namespace FarmAd.Persistence.Services.User
{
    public class ProductEditServices : IProductEditServices
    {
        private readonly IStorageService _storageService;
        private readonly IImageManagerService _ımageManagerService;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductEditServices(IStorageService storageService, IImageManagerService ımageManagerService, IProductImageWriteRepository productImageWriteRepository, IProductImageReadRepository productImageReadRepository, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _storageService = storageService;
            _ımageManagerService = ımageManagerService;
            _productImageWriteRepository = productImageWriteRepository;
            _productImageReadRepository = productImageReadRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task ProductDisabled(int id)
        {
            if (id == 0)
                throw new ItemNotFoundException("Elan tapılmadı");


            var product = await _productReadRepository.GetAsync(x => x.Id == id);

            if (product == null)
                throw new ItemNotFoundException("Elan tapılmadı");

            if (product.ProductFeatures.ProductStatus != ProductStatus.Active)
                throw new ItemFormatException("Xəta baş verdi");

            product.ProductFeatures.ProductStatus = ProductStatus.DeActive;
            product.ProductFeatures.IsPremium = false;
            product.ProductFeatures.IsVip = false;
            product.ProductFeatures.ExpirationDatePremium = DateTime.MinValue;
            product.ProductFeatures.ExpirationDateVip = DateTime.MinValue;
            product.ProductFeatures.ExpirationDateActive = DateTime.MinValue;

            await _productWriteRepository.SaveAsync();
        }
        public async Task ProductActive(int id)
        {
            if (id == 0)
                throw new ArgumentException("İD düzgün deyil!");

            var product = await _productReadRepository.GetAsync(x => x.Id == id);

            if (product == null)
                throw new ItemNotFoundException("Elan tapılmadı");

            DateTime now = DateTime.UtcNow;

            if (product.ProductFeatures.ExpirationDateDisabled < now)
                throw new ExpirationDateException("Elanın müddəti bitmişdir");

            if (product.ProductFeatures.ProductStatus != ProductStatus.DeActive)
                throw new ItemFormatException("Xəta baş verdi");

            product.ProductFeatures.ProductStatus = ProductStatus.Active;
            product.ProductFeatures.ExpirationDateActive = now.AddDays(30);

            await _productWriteRepository.SaveAsync();
        }
        public void ProductEditCheck(AdminProductEditPostDto product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Məhsul null ola bilməz!");

            //if (product.ProductFeatures == null)
            //    throw new ArgumentNullException(nameof(product.ProductFeatures), "ProductFeatures null ola bilməz!");

            if (string.IsNullOrWhiteSpace(product.Describe))
                throw new ItemNullException("Təsvir hissəsi boş ola bilməz!");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ItemNullException("Elanın adı boş ola bilməz!");

            if (product.Price <= 0)
                throw new ItemNullException("Elanın qiyməti 0₼-dan çox olmalıdır!");
        }
        public async Task ProductEdit(AdminProductEditPostDto productDto)
        {
            if (productDto == null || productDto.Id == 0)
                throw new ItemNotFoundException("Elan tapılmadı");

            Product oldProduct = await _productReadRepository.GetAsync(x => x.Id == productDto.Id, "ProductFeatures", "ProductImages");

            if (oldProduct == null)
                throw new ItemNotFoundException("Elan tapılmadı");

            bool hasChanges = false;

            if (productDto.ImageFiles != null)
                _ımageManagerService.ValidateImages(productDto.ImageFiles);

            // Məlumatların dəyişiklik yoxlanışı və yenilənməsi
            if (oldProduct.ProductFeatures.SubCategoryId != 0 && oldProduct.ProductFeatures.SubCategoryId != oldProduct.ProductFeatures.SubCategoryId)
            {
                oldProduct.ProductFeatures.SubCategoryId = productDto.SubCategoryId;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(productDto.Name) && oldProduct.ProductFeatures.Name != productDto.Name)
            {
                oldProduct.ProductFeatures.Name = productDto.Name;
                hasChanges = true;
            }

            if (!string.IsNullOrWhiteSpace(productDto.Describe) && oldProduct.ProductFeatures.Describe != productDto.Describe)
            {
                oldProduct.ProductFeatures.Describe = productDto.Describe;
                hasChanges = true;
            }

            if (productDto.Price > 0 && oldProduct.ProductFeatures.Price != productDto.Price)
            {
                oldProduct.ProductFeatures.Price = productDto.Price;
                hasChanges = true;
            }

            if (oldProduct.ProductFeatures.PriceCurrency != productDto.PriceCurrency)
            {
                oldProduct.ProductFeatures.PriceCurrency = productDto.PriceCurrency;
                hasChanges = true;
            }

            if (oldProduct.ProductFeatures.IsShipping != productDto.IsShipping)
            {
                oldProduct.ProductFeatures.IsShipping = productDto.IsShipping;
                hasChanges = true;
            }

            if (oldProduct.ProductFeatures.IsNew != productDto.IsNew)
            {
                oldProduct.ProductFeatures.IsNew = productDto.IsNew;
                hasChanges = true;
            }

            // Şəkil dəyişikliklərini yoxla
            int deleteCount = await DeleteImages(productDto, oldProduct);
            if (deleteCount > 0)
                hasChanges = true;

            if (await PosterImageChange(productDto, oldProduct) == 1)
                hasChanges = true;

            if (await CreateImagesFormFile(productDto.ImageFiles, productDto.Id, deleteCount) == 1)
                hasChanges = true;

            // Dəyişiklik baş veribsə, yadda saxla
            if (hasChanges)
            {
                oldProduct.ModifiedDate = DateTime.UtcNow.AddHours(4);
                oldProduct.ProductFeatures.ProductStatus = ProductStatus.Waiting;
                await _productWriteRepository.SaveAsync();
            }
        }
        private async Task<int> PosterImageChange(AdminProductEditPostDto productDto, Product productExist)
        {
            if (productDto.PosterImageFile != null)
            {
                var productImageFile = productDto.PosterImageFile;

                ProductImage ProductImage = productExist.ProductImages.FirstOrDefault(x => x.IsProduct);

                if (ProductImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                await _storageService.DeleteAsync("files\\Products", ProductImage.Image);

                var (filename, path) = _storageService.Upload("files\\Products", productImageFile);
                ProductImage.Image = filename;
                ProductImage.IsProduct = true;
                return 1;
            }
            return 0;

        }
        private async Task<int> DeleteImages(AdminProductEditPostDto productDto, Product productExist)
        {
            int i = 0;
            ICollection<ProductImage> productImages = productExist.ProductImages;

            List<int> imageIds = productDto.ProductImagesIds
                            .Split(',') // Stringi virgüllə ayırırıq
                            .Select(id => int.Parse(id)) // Her elementi int'e çeviririk
                            .ToList();

            if (productDto.ProductImagesIds != null)
            {
                foreach (var image in productImages.ToList()
                    .Where(x => !x.IsDelete && !x.IsProduct && !imageIds.Contains(x.Id)))
                {
                    await _storageService.DeleteAsync("files\\Products", image.Image);
                    productExist.ProductImages.Remove(image);
                    i++;
                }
            }
            else if (productDto.ImageFiles?.Count() > 0 || productImages.Any(x => !x.IsProduct))
            {
                foreach (var item in productImages.ToList().Where(x => !x.IsDelete && !x.IsProduct))
                {
                    await _storageService.DeleteAsync("files\\Products", item.Image);
                    productExist.ProductImages.Remove(item);
                    i++;
                }
            }
            else if (!productImages.Any(x => x.IsProduct))
                throw new ImageCountException("Axırıncı şəkil silinə bilməz!");

            return i;
        }
        private async Task<int> CreateImagesFormFile(List<IFormFile> imageFiles, int productId, int deleteCount)
        {
            int countImage = await _productImageReadRepository.GetTotalCountAsync(x => x.ProductId == productId && !x.IsProduct);
            int maxImageCount = 8;
            int remainingSlots = maxImageCount - countImage - deleteCount;

            if (remainingSlots <= 0) throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz!");

            List<ProductImage> imageList = new();
            if (imageFiles != null)
                imageList = _ımageManagerService.AddImages(productId, imageFiles,true);
            if (imageList.Count > 0)
            {
                await _productImageWriteRepository.AddRangeAsync(imageList);
                return 1;
            }
            return 0;
        }

    }
}
