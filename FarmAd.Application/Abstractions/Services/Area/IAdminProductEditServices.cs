using FarmAd.Domain.Entities;
using FarmAd.Application.DTOs.Area;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IAdminProductEditServices
    {
        public void CheckPostEdit(Product Product);
        public Task EditProduct(Product Product);
        public Task ProductActive(int id);
        public Task ProductDisabled(int id);

    }
}
