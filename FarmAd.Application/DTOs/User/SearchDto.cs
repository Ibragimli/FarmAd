using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class SearchDto
    {
        public string PosterName { get; set; } = null;
        public int? CategoryId { get; set; } = null;
        public int? SubCategoryId { get; set; } = null;
        public int? CityId { get; set; } = null;
        public int Page { get; set; } = 1;
    }
}
