using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class WishDto
    {
        public List<WishItemDto> WishItems { get; set; }

    }
    public class WishItemDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        
    }
}
