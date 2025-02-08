using FarmAd.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int ProductFeatureId { get; set; }
        public ProductFeature ProductFeatures { get; set; }
        public ICollection<ProductUserId> ProductUserIds { get; set; }
        public ICollection<WishItem> WishItems { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile ProductImageFile { get; set; }

        [NotMapped]
        public List<int> ProductImagesIds { get; set; }


    }
}
