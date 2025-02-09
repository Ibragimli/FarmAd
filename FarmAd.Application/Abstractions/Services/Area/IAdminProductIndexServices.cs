using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Interfaces.Area
{
    public interface IAdminProductIndexServices
    {
        public IQueryable<Product> GetProduct(string name, string phoneNumber, int subCategoryId);
        public Task<List<SubCategory>> GetSubCategories();
        public Task<List<Category>> GetCategories();
        public Task IsDisabled();


    }
}
