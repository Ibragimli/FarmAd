using FarmAd.Domain.Entities;
using FarmAd.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductCreateIndexServices
    {
        public ProductCreateDto ProductDto();
        public Task<List<City>> Cities();
        public Task<List<Category>> Categories();
        public Task<List<SubCategory>> SubCategories();

    }
}
