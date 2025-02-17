using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.DTOs.Area
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
