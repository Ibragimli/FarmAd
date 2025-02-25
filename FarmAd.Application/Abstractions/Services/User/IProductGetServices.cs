using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductGetServices
    {
        Task<(IList<Product>, int)> Products(string name, int page, int size);

    }
}
