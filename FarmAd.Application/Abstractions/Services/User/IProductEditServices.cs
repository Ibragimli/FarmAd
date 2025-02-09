using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductEditServices
    {
        public Task ProductDisabled(int id);
        public Task ProductActive(int id);
        public void ProductEditCheck(Product Product);
        public Task ProductEdit(Product Product );
    }
}
