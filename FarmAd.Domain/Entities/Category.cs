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
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile CategoryImageFile { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
