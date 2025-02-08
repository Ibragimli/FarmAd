using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class WishPosterCreateDto
    {
        public List<WishItemsDto> WishItems { get; set; }

    }
    public class WishItemsDto
    {
        public int PosterId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}
