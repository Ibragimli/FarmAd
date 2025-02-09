using FarmAd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FarmAd.Application.Abstractions.Services.User
{
    public interface IProductDetailIndexServices
    {
        public Task<Product> GetProduct(int id);
        public Task ProductViewCount(Product Product);
        public Task<ProductUserId> GetUser(int id);
        public Task<List<Product>> GetSimilarProduct(int id, Product Product);
        public Task<List<ServiceDuration>> GetServiceDurations();
        public Task<int> GetWishCount(int id);


    }
}
