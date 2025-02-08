using FarmAd.Domain.Entities.Common;
using FarmAd.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class ProductFeature : BaseEntity
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? Price { get; set; }
        public bool PriceCurrency { get; set; }
        public int ViewCount { get; set; }
        public int WishCount { get; set; }
        public bool IsVip { get; set; }
        public bool IsNew { get; set; }
        public bool IsShipping { get; set; }
        public bool IsPremium { get; set; }
        public ProductStatus ProductStatus { get; set; }

        public DateTime ExpirationDateVip { get; set; }
        public DateTime ExpirationDatePremium { get; set; }
        public DateTime ExpirationDateDisabled { get; set; } = DateTime.UtcNow.AddHours(4).AddDays(90);
        public DateTime ExpirationDateActive { get; set; } = DateTime.UtcNow.AddHours(4).AddDays(30);
        public bool IsDisabled { get; set; }
        public int SubCategoryId { get; set; }
        public int CityId { get; set; }
        public SubCategory SubCategory { get; set; }
        public City City { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
