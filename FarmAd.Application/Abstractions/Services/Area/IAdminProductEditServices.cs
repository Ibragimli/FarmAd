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
        public Task CheckPostEdit(AdminProductEditPostDto product);
        public Task EditProduct(AdminProductEditPostDto product);
        public Task ProductActive(int id);
        public Task ProductDisabled(int id);

    }
}
