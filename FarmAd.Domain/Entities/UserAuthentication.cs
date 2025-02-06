using FarmAd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Domain.Entities
{
    public class UserAuthentication : BaseEntity
    {
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public bool IsDisabled { get; set; }
        public int Count { get; set; }
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddHours(4).AddMinutes(10);
    }
}
