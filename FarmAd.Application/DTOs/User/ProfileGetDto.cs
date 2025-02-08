using FarmAd.Domain.Entities;
using FarmAd.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FarmAd.Application.DTOs.User
{
    public class ProfileGetDto
    {
        public AppUser User { get; set; }
        public ProfileEditDto ProfileEditDto { get; set; }
        public IEnumerable<Product> ActiveProducts { get; set; }
        public IEnumerable<Product> DeactiveProducts { get; set; }
        public IEnumerable<Product> WaitedProducts { get; set; }
        public IEnumerable<Product> DisabledProducts { get; set; }
        public IEnumerable<Payment> PersonalPayments { get; set; }
        public IEnumerable<Payment> ProductPayments { get; set; }
        public BalanceDto BalanceDto { get; set; }
    }
}
