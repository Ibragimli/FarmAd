    using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Image { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }
        public bool IsProduct { get; set; }
        public Product Product { get; set; }
    }
}
