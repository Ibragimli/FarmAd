using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class ProductEditGetDto
    {
        public Product Product { get; set; }
        public List<City> Cities { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public AdminProductEditPostDto ProductEditDto { get; set; }
    }
}
