using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminProductDetailIndexServices
    {
        public Task<Product> GetProduct(int id);
        public Task<List<SubCategory>> GetSubCategories();
        public Task<List<Category>> GetCategories();
        public Task<List<City>> GetAllCity();
        public Task<ProductUserId> GetAppUser(int ProductId);

    }
}
