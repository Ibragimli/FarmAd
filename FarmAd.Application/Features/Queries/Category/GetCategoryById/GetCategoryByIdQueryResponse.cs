using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Features.Queries.Category.GetCategoryById
{
    public class GetCategoryByIdQueryResponse
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }
    }
}
