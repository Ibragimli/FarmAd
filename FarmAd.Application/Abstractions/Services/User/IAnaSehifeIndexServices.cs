using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace FarmAd.Application.Abstractions.Services.User
namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IAnaSehifeIndexServices
    {
        public IQueryable<Product> GetProductsAsync();
        public IQueryable<Product> GetVipProductAsync();
        public IQueryable<Product> GetPremiumProductAsync();
        public IQueryable<Product> GetPreProductAsync();
        
        public Task<List<Category>> GetAllCategoryAsync();

    }
}
