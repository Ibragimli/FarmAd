using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IUserProductsServices
    {
        public IQueryable<Product> VipProducts(string phoneNumber);
        public IQueryable<Product> AllProducts(string phoneNumber);
        public IQueryable<Product> PremiumProducts(string phoneNumber);
        
    }
}
