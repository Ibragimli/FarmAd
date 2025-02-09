using FarmAd.Application.DTOs.User;
using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductSearchServices
    {
        public IQueryable<Product> SearchProductAll(SearchDto searchDto);
        public IQueryable<Product> SearchProductVip(SearchDto searchDto);
        public IQueryable<Product> SearchProductPremium(SearchDto searchDto);
    }
}
