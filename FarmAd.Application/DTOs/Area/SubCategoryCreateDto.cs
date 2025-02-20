using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.DTOs.Area
{
    public class SubCategoryCreateDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
