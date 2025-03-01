using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities.Identity
{
    public class AppRole:IdentityRole<string>
    {
        public AppRole() : base()
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
            ConcurrencyStamp = Guid.NewGuid().ToString();
        }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
