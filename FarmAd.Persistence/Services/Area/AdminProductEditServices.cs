using FarmAd.Domain.Entities;
using FarmAd.Application.Exceptions;
using Ferma.Service.Services.Interfaces.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FarmAd.Domain.Enums;
using FarmAd.Application.Repositories.Product;

namespace Ferma.Service.Services.Implementations.Area
{
    public class AdminProductEditServices : IAdminProductEditServices
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public AdminProductEditServices(IProductReadRepository productReadRepository,IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public void CheckPostEdit(Product Product)
        {
            if (Product.Id == 0)
                throw new NotFoundException("Error");
            if (Product.ProductFeatures.SubCategoryId == 0)
                throw new ItemNullException("Kategoriya hissəsi boş ola bilməz");
            if (Product.ProductFeatures.Name == null)
                throw new ItemNullException("Elanın ad hissəsi boş ola bilməz");
            if (Product.ProductFeatures.Describe == null)
                throw new ItemNullException("Təsvir hissəsi boş ola bilməz");
            if (Product.ProductFeatures.Name.Length > 100)
                throw new ItemNullException("Elanın ad hissəsinin uzunluğu  maksimum 100  ola bilər");
            if (Product.ProductFeatures.Describe.Length > 3000)
                throw new ItemNullException("Elanın ad hissəsinin uzunluğu  maksimum 3000  ola bilər");

        }

        public async Task EditProduct(Product Product)
        {
            var oldProduct = await _productReadRepository.GetAsync(x => x.Id == Product.Id, "ProductFeatures");
            if (oldProduct == null)
                throw new NotFoundException("Error");

            oldProduct.ProductFeatures.Name = Product.ProductFeatures.Name;
            oldProduct.ProductFeatures.Describe = Product.ProductFeatures.Describe;
            oldProduct.ProductFeatures.SubCategoryId = Product.ProductFeatures.SubCategoryId;
            await _productWriteRepository.SaveAsync();
        }
        public async Task ProductDisabled(int id)
        {
            Product Product = new Product();
            if (id != 0)
                Product = await _productReadRepository.GetAsync(x => x.Id == id);

            var time = new DateTime(0001, 01, 01, 8, 36, 44);

            if (Product == null)
                throw new ItemNotFoundException("Elan tapılmadı");
            Product.ProductFeatures.ProductStatus = ProductStatus.Disabled;
            Product.ProductFeatures.IsPremium = false;
            Product.ProductFeatures.IsVip = false;
            Product.ProductFeatures.ExpirationDatePremium = time;
            Product.ProductFeatures.ExpirationDateVip = time;
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

            Product.ProductFeatures.ProductStatus = ProductStatus.Active;
            Product.ProductFeatures.ExpirationDateActive = now.AddDays(30);
            Product.ProductFeatures.ExpirationDateDisabled = now.AddDays(90);
            Product.ProductFeatures.IsDisabled = false;

            await _productWriteRepository.SaveAsync();

        }

    }
}
