using FarmAd.Domain.Entities.Common;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace FarmAd.Domain.Entities
{
    public class Payment : BaseEntity
    {
        //public PaymentService Service { get; set; }
        //public Source Source { get; set; }
        //public ServiceType ServiceType { get; set; }
        public decimal? Amount { get; set; }
        public int? Duration { get; set; }
        public string AppUserId { get; set; }
        public int? PosterId { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
