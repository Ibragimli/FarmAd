using FarmAd.Application.DTOs.User;
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
        public void ProductEditCheck(AdminProductEditPostDto Product);
        public Task ProductEdit(AdminProductEditPostDto Product );
    }
}
