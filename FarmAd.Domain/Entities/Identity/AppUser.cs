using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
       
        public bool IsAdmin { get; set; }
        public decimal? Balance { get; set; }
        public ICollection<ProductUserId> ProductUserIds { get; set; }
        public ICollection<WishItem> WishItems { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }
}
