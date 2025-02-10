using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.Area
{
    public interface IProductDeleteServices
    {
        public Task DeleteProduct(int id);
    }
}
