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

namespace FarmAd.Persistence.Service.User
{
    public class ProductEditServices : IProductEditServices
    {
        private readonly IImageManagerService _manageImageHelper;
        private readonly IProductImageWriteRepository _productImageWriteRepository;
        private readonly IProductImageReadRepository _productImageReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public ProductEditServices(IImageManagerService manageImageHelper, IProductImageWriteRepository productImageWriteRepository, IProductImageReadRepository productImageReadRepository, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _manageImageHelper = manageImageHelper;
            _productImageWriteRepository = productImageWriteRepository;
            _productImageReadRepository = productImageReadRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task ProductDisabled(int id)
        {
            Product Product = new Product();
            if (id != 0)
                Product = await _productReadRepository.GetAsync(x => x.Id == id);

            var time = new DateTime(0001, 01, 01, 8, 36, 44);

            if (Product == null)
                throw new ItemNotFoundException("Elan tapılmadı");
            if (Product.ProductFeatures.ProductStatus != ProductStatus.Active)
                throw new ItemFormatException("Xəta baş verdi");

            Product.ProductFeatures.ProductStatus = ProductStatus.DeActive;
            Product.ProductFeatures.IsPremium = false;
            Product.ProductFeatures.IsVip = false;
            Product.ProductFeatures.ExpirationDatePremium = time;
            Product.ProductFeatures.ExpirationDateVip = time;
            Product.ProductFeatures.ExpirationDateActive = time;
            await _productWriteRepository.SaveAsync();
        }
        public async Task ProductActive(int id)
        {
            Product Product = new Product();
            DateTime now = DateTime.UtcNow;
            if (id != 0)
                Product = await _productReadRepository.GetAsync(x => x.Id == id);

            if (Product == null)
                throw new ItemNotFoundException("Elan tapılmadı");
            if (Product.ProductFeatures.ExpirationDateDisabled < now)
                throw new ExpirationDateException("Elanın müddəti bitmişdir");
            if (Product.ProductFeatures.ProductStatus != ProductStatus.DeActive)
                throw new ItemFormatException("Xəta baş verdi");

            Product.ProductFeatures.ProductStatus = ProductStatus.Active;
            Product.ProductFeatures.ExpirationDateActive = now.AddDays(30);
            await _productWriteRepository.SaveAsync();

        }
        public void ProductEditCheck(Product Product)
        {
            if (Product.ProductFeatures.Describe == null)
                throw new ItemNullException("Təsvir hissəsi boş ola bilməz!");
            if (Product.ProductFeatures.Name == null)
                throw new ItemNullException("Elanın adı boş ola bilməz!");
            if (Product.ProductFeatures.Price == 0 || Product.ProductFeatures.Price == null)
                throw new ItemNullException("Elanın qiyməti 0₼-dan çox olmalıdır!");
        }

        public async Task ProductEdit(Product Product)
        {
            bool checkBool = false;
            if (Product == null)
                throw new ItemNotFoundException("Elan tapılmadı");

            if (Product.Id == 0)
                throw new ItemNotFoundException("Elan tapılmadı");
            var oldProduct = await _productReadRepository.GetAsync(x => x.Id == Product.Id, "ProductFeatures", "ProductImages");

            if (Product.ProductImageFile != null)
                _manageImageHelper.ValidateProduct(Product.ProductImageFile);
            if (Product.ImageFiles != null)
                _manageImageHelper.ValidateImages(Product.ImageFiles);


            if (Product.ProductFeatures.SubCategoryId != 0 && oldProduct.ProductFeatures.SubCategoryId != Product.ProductFeatures.SubCategoryId)
            {
                oldProduct.ProductFeatures.SubCategoryId = Product.ProductFeatures.SubCategoryId;
                checkBool = true;
            }
            if (Product.ProductFeatures.Name != null && oldProduct.ProductFeatures.Name != Product.ProductFeatures.Name)
            {
                oldProduct.ProductFeatures.Name = Product.ProductFeatures.Name;
                checkBool = true;
            }
            if (Product.ProductFeatures.Describe != null && oldProduct.ProductFeatures.Describe != Product.ProductFeatures.Describe)
            {
                oldProduct.ProductFeatures.Describe = Product.ProductFeatures.Describe;
                checkBool = true;
            }
            if (Product.ProductFeatures.Price != 0 && oldProduct.ProductFeatures.Price != Product.ProductFeatures.Price)
            {
                oldProduct.ProductFeatures.Price = Product.ProductFeatures.Price;
                checkBool = true;
            }
            if (oldProduct.ProductFeatures.PriceCurrency != Product.ProductFeatures.PriceCurrency)
            {
                oldProduct.ProductFeatures.PriceCurrency = Product.ProductFeatures.PriceCurrency;
                checkBool = true;
            }
            if (oldProduct.ProductFeatures.IsShipping != Product.ProductFeatures.IsShipping)
            {
                oldProduct.ProductFeatures.IsShipping = Product.ProductFeatures.IsShipping;
                checkBool = true;
            }
            if (oldProduct.ProductFeatures.IsNew != Product.ProductFeatures.IsNew)
            {
                oldProduct.ProductFeatures.IsNew = Product.ProductFeatures.IsNew;
                checkBool = true;
            }
            int deleteCount = DeleteImages(Product, oldProduct);
            if (deleteCount > 0)
                checkBool = true;
            if (ProductImageChange(Product, oldProduct) == 1)
                checkBool = true;
            if (await CreateImageFormFile(Product.ImageFiles, Product.Id, deleteCount) == 1)
                checkBool = true;

            if (checkBool)
            {
                oldProduct.ModifiedDate = DateTime.UtcNow.AddHours(4);
                oldProduct.ProductFeatures.ProductStatus = ProductStatus.Waiting;
                await _productWriteRepository.SaveAsync();

            }


        }
        private int ProductImageChange(Product Product, Product ProductExist)
        {
            if (Product.ProductImageFile != null)
            {
                var ProductImageFile = Product.ProductImageFile;

                ProductImage ProductImage = ProductExist.ProductImages.FirstOrDefault(x => x.IsProduct);

                if (ProductImage == null) throw new ImageNullException("Şəkil tapılmadı!");

                string filename = _manageImageHelper.FileSave(ProductImageFile, "Product");
                _manageImageHelper.DeleteFile(ProductImage.Image, "Product");
                ProductImage.Image = filename;
                ProductImage.IsProduct = true;
                return 1;
            }
            return 0;

        }
        private int DeleteImages(Product Product, Product ProductExist)
        {
            int i = 0;
            ICollection<ProductImage> ProductImages = ProductExist.ProductImages;
            if (Product.ProductImagesIds != null)
            {
                foreach (var image in ProductImages.ToList().Where(x => x.IsDelete == false && !x.IsProduct && !Product.ProductImagesIds.Contains(x.Id)))
                {
                    _manageImageHelper.DeleteFile(image.Image, "Product");
                    ProductExist.ProductImages.Remove(image);
                    i++;
                }
                ProductImages.ToList().RemoveAll(x => !Product.ProductImagesIds.Contains(x.Id));
                return i;
            }
            else
            {

                if (Product.ImageFiles?.Count() > 0)
                {
                    foreach (var item in ProductImages.ToList().Where(x => !x.IsDelete && !x.IsProduct))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "Product");
                        ProductExist.ProductImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (ProductImages.Any(x => !x.IsProduct))
                {
                    foreach (var item in ProductImages.ToList().Where(x => !x.IsDelete && !x.IsProduct))
                    {
                        _manageImageHelper.DeleteFile(item.Image, "Product");
                        ProductExist.ProductImages.Remove(item);
                        i++;
                    }
                    return i;
                }
                else if (ProductImages.Any(x => x.IsProduct))
                {
                    return i;
                }
                else throw new ImageCountException("Axırıncı şəkil silinə bilməz!");
            }

        }
        private async Task<int> CreateImageFormFile(List<IFormFile> imageFiles, int ProductId, int deleteCount)
        {
            int countImage = await _productImageReadRepository.GetTotalCountAsync(x => x.ProductId == ProductId && !x.IsProduct);
            int i = 0;

            if (countImage < 9)
            {
                if (imageFiles != null)
                {
                    i = 8 - countImage - deleteCount;
                    if (i == 0)
                        throw new ImageCountException("Maksimum 8 şəkil əlavə edə bilərsiniz!");
                    foreach (var image in imageFiles)
                    {
                        if (i != 0)
                        {
                            ProductImage Productimage = new ProductImage
                            {
                                IsProduct = false,
                                ProductId = ProductId,
                                Image = _manageImageHelper.FileSave(image, "Product"),
                            };
                            await _productImageWriteRepository.AddAsync(Productimage);
                            i--;
                        }
                    }
                    return 1;
                }
            }
            return 0;
        }
    }
}
