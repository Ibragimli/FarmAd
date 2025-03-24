using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminProductIndexServices
    {
        public Task<(IQueryable<Product>, int)> GetProducts(string name, string phoneNumber, int subCategoryId);
        public Task<List<SubCategory>> GetSubCategories();
        public Task<List<Category>> GetCategories();
        public Task DisableExpiredProducts();


    }
}
